/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 18, 2024
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
    public static class Vector3Extensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Vector3Int ToVector3Int(this Vector3 vector)
        {
            return new Vector3Int((int) vector.x, (int) vector.y, (int) vector.z);
        }

        public static float GetAxisValue(this Vector3 vector, Axis axis)
        {
            switch (axis)
            {
                case Axis.XPos:
                case Axis.X:
                {
                    return vector.x;
                }
                case Axis.YPos:
                case Axis.Y:
                {
                    return vector.y;
                }
                case Axis.ZPos:
                case Axis.Z:
                {
                    return vector.z;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            };
        }

        public static void SetAxisValue(this ref Vector3 vector, Axis axis, float value)
        {
            switch (axis)
            {
                case Axis.XPos:
                case Axis.X:
                {
                    vector.x += value;
                    break;
                }
                case Axis.YPos:
                case Axis.Y:
                {
                    vector.y += value;
                    break;
                }
                case Axis.ZPos:
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

        /// <summary>
        /// reduces `vector` by an opposite direction vector of magnitude `magnitude`; returns Vector3.zero if the magnitude is greater
        /// </summary>
        public static void ReduceBy(this ref Vector3 vector, float magnitude)
        {
            Vector3 newVector = vector + -vector.normalized * magnitude;
            vector = Vector3.Dot(vector, newVector) < 0 ? Vector3.zero : newVector;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}