/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 18, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities
{
    public static class Vector3IntExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static Vector3 ToVector3(this Vector3Int vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

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
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        public static Vector3Int GetDirection(Vector3Int from, Vector3Int to)
        {
            return to - from;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}