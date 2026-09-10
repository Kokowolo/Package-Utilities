/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
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
        [SerializeField] Transform _Target = null;
        
        [Header("Settings")]
        [SerializeField] protected FaceDirection faceDirection = FaceDirection.Z;
        [SerializeField] protected bool faceAwayFrom = true;
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

        protected virtual void OnEnable()
        {
            if (!Target)
            {
                LogManager.LogError($"{nameof(Target)} has not been set");
            }
            LateUpdate();
        }

        protected virtual void LateUpdate()
        {
            switch (faceDirection)
            {
                case FaceDirection.X:
                {
                    eulerAngles = new Vector3(
                        TargetEulerAngles.z, 
                        TargetEulerAngles.y + 90,
                        faceAwayFrom ? TargetEulerAngles.x + 180 : TargetEulerAngles.x
                    );
                    break;
                }
                case FaceDirection.Y:
                {
                    eulerAngles = new Vector3(
                        faceAwayFrom ? TargetEulerAngles.x + 90 : TargetEulerAngles.x - 90, 
                        TargetEulerAngles.y, 
                        TargetEulerAngles.z
                    );
                    break;
                }
                case FaceDirection.Z:
                {
                    eulerAngles = new Vector3(
                        faceAwayFrom ? TargetEulerAngles.x : TargetEulerAngles.x + 180, 
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