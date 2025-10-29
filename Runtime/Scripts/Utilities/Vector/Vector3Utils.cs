/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 3, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class Vector3Utils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

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
        /*██████████████████████████████████████████████████████████*/
    }
}