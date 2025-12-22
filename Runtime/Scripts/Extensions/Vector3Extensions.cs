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

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

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
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        public static Vector3 Abs(Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }

        public static Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            return to - from;
        }

        public static Vector3 GetDirectionNormalized(Vector3 from, Vector3 to)
        {
            return GetDirection(from, to).normalized;
        }

        public static Vector3 SetComponentInDirection(Vector3 vector, Vector3 direction, float value)
        {
            return SetComponentInNormalizedDirection(vector, direction.normalized, value);
        }

        public static Vector3 SetComponentInNormalizedDirection(Vector3 vector, Vector3 direction, float value)
        {
            return vector + (value - GetComponentInNormalizedDirection(vector, direction)) * direction;
        }

        /// <summary>
        /// Gets `a`'s scalar component in direction `b`; NOTE: multiplying this scalar component by a normalized `b` would yield that 
        /// scalar component as a vector 
        /// </summary>
        public static float GetComponentInDirection(Vector3 vector, Vector3 direction)
        {
            return GetComponentInNormalizedDirection(vector, direction.normalized);
        }

        /// <summary>
        /// Gets `a`'s scalar component in direction `b`; NOTE: multiplying this scalar component by a normalized `b` would yield that 
        /// scalar component as a vector 
        /// </summary>
        public static float GetComponentInNormalizedDirection(Vector3 vector, Vector3 direction)
        {
            // a * cosØ == a ⋅ b / |b|
            float dot = Vector3.Dot(vector, direction);
            // float component = dot / direction.magnitude;
            return dot;
        }

        /// <summary>
        /// Aligns a direction vector along a plane using its normal; see more:
        /// https://catlikecoding.com/unity/tutorials/movement/physics/slopes/projecting-vector.png
        /// </summary>
        public static Vector3 ProjectOnPlane(Vector3 direction, Vector3 normal) 
        {
            return (direction - normal * Vector3.Dot(direction, normal)).normalized;
            // NOTE: Why not use Vector3.ProjectOnPlane? That method does the same but doesn't assume that the provided normal vector is of unit 
            //       length. It divides the result by the squared length of the normal, which is always 1, so it's not needed.
        }

        /// <summary>
        /// reduces `vector` by an opposite direction vector of magnitude `magnitude`; returns Vector3.zero if the magnitude is greater
        /// </summary>
        public static Vector3 ReduceBy(Vector3 vector, float magnitude)
        {
            Vector3Extensions.ReduceBy(ref vector, magnitude);
            return vector;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}