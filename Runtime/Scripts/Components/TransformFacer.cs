/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 19, 2023
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Forces a GameObject to face a Transform
    /// </summary>
    [DisallowMultipleComponent]
    public class TransformFacer : MonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Enums

        protected enum FaceDirection
        {
            X,
            Y,
            Z
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Header("References")]
        [SerializeField] private Transform _Target = null;
        
        [Header("Settings")]
        [SerializeField] protected FaceDirection faceDirection = FaceDirection.Y;
        [SerializeField] protected bool faceAwayFromCamera = false;
        [SerializeField] protected Quaternion _Offset;

        Vector3 eulerAngles;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public Transform Target 
        {
            get => _Target;
            set => _Target = value;
        }

        public Quaternion Offset 
        {
            get => _Offset;
            set => _Offset = value;
        }

        public Vector3 TargetEulerAngles => Target.transform.eulerAngles;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        protected virtual void Start() 
        {
            if (!Target)
            {
                LogManager.LogError($"{nameof(Target)} has not been set");
            }
        }

        // TODO: convert this to Quaternion, otherwise it looks like there is gimbal lock
        protected virtual void FixedUpdate()
        {
            switch (faceDirection)
            {
                case FaceDirection.X:
                {
                    eulerAngles = new Vector3(
                        TargetEulerAngles.z, 
                        TargetEulerAngles.y + 90,
                        faceAwayFromCamera ? TargetEulerAngles.x + 180 : TargetEulerAngles.x
                    );
                    break;
                }
                case FaceDirection.Y:
                {
                    eulerAngles = new Vector3(
                        faceAwayFromCamera ? TargetEulerAngles.x + 90 : TargetEulerAngles.x - 90, 
                        TargetEulerAngles.y, 
                        TargetEulerAngles.z
                    );
                    break;
                }
                case FaceDirection.Z:
                {
                    eulerAngles = new Vector3(
                        faceAwayFromCamera ? TargetEulerAngles.x : TargetEulerAngles.x + 180, 
                        TargetEulerAngles.y, 
                        TargetEulerAngles.z
                    );
                    break;
                }
            }

            transform.eulerAngles = eulerAngles + Offset.eulerAngles;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}