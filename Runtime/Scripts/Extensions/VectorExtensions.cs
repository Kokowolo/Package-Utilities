/*
 * File Name: VectorExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 18, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class VectorExtensions
    {
        /************************************************************/
        #region Functions

        public static Vector3Int ToVector3Int(this Vector3 v)
        {
            return new Vector3Int((int) v.x, (int) v.y, (int) v.z);
        }

        public static Vector3 ToVector3(this Vector2 v)
        {
            return new Vector3(v.x, v.y);
        }

        #endregion
        /************************************************************/
    }
}