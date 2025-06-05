/*
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
    public static class Vector2Extensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }

        public static Vector3Int ToVector3Int(this Vector2 vector)
        {
            return new Vector3Int((int) vector.x, (int) vector.y, 0);
        }

        public static float GetAxisValue(this Vector2 vector, Axis axis)
        {
            return axis switch
            {
                Axis.X => vector.x,
                Axis.Y => vector.y,
                _ => throw new NotImplementedException(),
            };
        }

        public static void SetAxisValue(this ref Vector2 vector, Axis axis, float value)
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
                default:
                {
                    throw new NotImplementedException();
                }
            };
        }

        public static void AddAxisValue(this ref Vector2 vector, Axis axis, float value)
        {
            vector.SetAxisValue(axis, vector.GetAxisValue(axis) + value);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}