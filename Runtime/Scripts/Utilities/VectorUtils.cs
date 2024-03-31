/*
 * File Name: VectorUtils.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 3, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class VectorUtils
    {
        /************************************************************/
        #region Functions

        public static Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            return to - from;
        }

        public static Vector3 GetDirectionNormalized(Vector3 from, Vector3 to)
        {
            return GetDirection(from, to).normalized;
        }

        /// <summary>
        /// Gets `a`'s scalar component in direction `b`; note multiplying this scalar component by a normalized `b` would yield that 
        /// scalar component as a vector 
        /// </summary>
        public static float GetComponentInDirection(Vector3 v, Vector3 direction)
        {
            // a * cosØ == a ⋅ b / |b|
            float dot = Vector3.Dot(v, direction);
            float component = dot / direction.magnitude;
            return component;
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

        #endregion
        /************************************************************/
    }
}