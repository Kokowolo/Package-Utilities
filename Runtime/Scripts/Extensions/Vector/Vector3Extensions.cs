/*
 * File Name: Vector3Extensions.cs
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

using System;

namespace Kokowolo.Utilities
{
    public static class Vector3Extensions
    {
        /************************************************************/
        #region Functions

        public static Vector3Int ToVector3Int(this Vector3 vector)
        {
            return new Vector3Int((int) vector.x, (int) vector.y, (int) vector.z);
        }

        public static float GetAxisValue(this Vector3 vector, Axis axis)
        {
            return axis switch
            {
                Axis.X => vector.x,
                Axis.Y => vector.y,
                Axis.Z => vector.z,
                _ => throw new NotImplementedException(),
            };
        }

        public static void SetAxisValue(this ref Vector3 vector, Axis axis, float value)
        {
            switch (axis)
            {
                case Axis.X:
                {
                    vector.x += value;
                    break;
                }
                case Axis.Y:
                {
                    vector.y += value;
                    break;
                }
                case Axis.Z:
                {
                    vector.z += value;
                    break;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            };
        }

        public static void AddAxisValue(this ref Vector3 vector, Axis axis, float value)
        {
            vector.SetAxisValue(axis, vector.GetAxisValue(axis) + value);
        }

        #endregion
        /************************************************************/
    }
}