/*
 * File Name: Vector3IntExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 18, 2024
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
    public static class Vector3IntExtensions
    {
        /************************************************************/
        #region Functions

        public static int GetAxisValue(this Vector3Int vector, Axis axis)
        {
            return axis switch
            {
                Axis.X => vector.x,
                Axis.Y => vector.y,
                Axis.Z => vector.z,
                _ => throw new NotImplementedException(),
            };
        }

        public static void SetAxisValue(this ref Vector3Int vector, Axis axis, int value)
        {
            switch (axis)
            {
                case Axis.X:
                {
                    vector.x = value;
                    break;
                }
                case Axis.Y:
                {
                    vector.y = value;
                    break;
                }
                case Axis.Z:
                {
                    vector.z = value;
                    break;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            };
        }

        public static void AddAxisValue(this ref Vector3Int vector, Axis axis, int value)
        {
            vector.SetAxisValue(axis, vector.GetAxisValue(axis) + value);
        }

        #endregion
        /************************************************************/
    }
}