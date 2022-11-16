/*
 * File Name: CameraFacer.cs
 * Description: This script is for forcing a GameObject (particularly UI) to face the Camera
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 29, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 *
 *      TODO: generalize this class to make it face a transform, i.e. TransformFacer or RotateTowardsTransform
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class CameraFacer : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [SerializeField] public Camera camera = null;

        #endregion
        /************************************************************/
        #region Functions

        private void Start() 
        {
            if (!camera)
            {
                LogManager.LogWarning("[CameraFacer] camera has not been set");   
                camera = Camera.main; 
            }
        }

        private void FixedUpdate()
        {
            transform.eulerAngles = camera.transform.eulerAngles;
        }

        #endregion
        /************************************************************/
    }
}