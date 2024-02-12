/*
 * File Name: QuaterionExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: December 29, 2023
 * 
 * Additional Comments:
 *		File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class QuaternionExtensions
    {
        /************************************************************/
        #region Functions

        public static Vector3 ToDirectionVector(this Quaternion rotation, Vector3 axis)
        {
            // taken from https://discussions.unity.com/t/how-can-i-convert-a-quaternion-to-a-direction-vector/80376
            return rotation * axis;
        }
        
        #endregion
        /************************************************************/
    }
}