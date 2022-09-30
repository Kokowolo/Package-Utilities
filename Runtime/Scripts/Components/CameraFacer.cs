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

        [SerializeField] Camera mainCamera = null;

        #endregion
        /************************************************************/
        #region Functions

        private void FixedUpdate()
        {
            transform.eulerAngles = mainCamera.transform.eulerAngles;
        }

        #endregion
        /************************************************************/
    }
}