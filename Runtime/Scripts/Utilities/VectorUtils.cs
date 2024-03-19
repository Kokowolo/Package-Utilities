/*
 * File Name: VectorUtils.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 3, 2024
 * 
 * Additional Comments:
 *		File Line Length: 140
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

        /// <summary>
        /// Gets `a`'s scalar component in direction `b`; as an aside, multiplying this scalar component by a normalized `b` would yield that 
        /// scalar component as a vector 
        /// </summary>
        public static float GetVectorComponentInDirection(Vector3 a, Vector3 b)
        {
            // a * cosØ == a ⋅ b / |b|
            float dot = Vector3.Dot(a, b);
            float component = dot / b.magnitude;
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