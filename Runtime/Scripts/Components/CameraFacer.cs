/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 29, 2022
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
    /// Forces a GameObject (particularly UI) to face the Camera
    /// </summary>
    public class CameraFacer : TransformFacer
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        private void OnValidate()
        {
            if (Target && !Target.GetComponent<Camera>())
            {
                LogManager.LogWarning($"{nameof(Target)} must be of type {nameof(Camera)}");   
                Target = null;
            }
        }

        protected override void Start() 
        {
            if (!Target)
            {
                LogManager.Log($"{nameof(Target)} has not been set, using Camera.main");   
                Target = Camera.main.transform; 
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}