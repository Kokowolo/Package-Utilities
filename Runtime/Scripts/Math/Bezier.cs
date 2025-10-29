/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Authors: Catlike Coding, Will Lacey
 * Date Created: October 8, 2020
 * 
 * Additional Comments: 
 *      The original version of this file can be found here: https://catlikecoding.com/unity/tutorials/hex-map/ within 
 *       Catlike Coding's tutorial series: Hex Map; this file has been updated it to better fit this repository
 *
 *       "A Bézier curve is defined by a sequence of points. It starts at the first point and ends at the last point,  
 *       but does not need to go through the intermediate points. Instead, those points pull the curve away from being a 
 *       straight line. [...] The idea of Bézier curves is that they are parametric. If you give it a value, typically 
 *       named t, between zero and one, you get a point on the curve. As t increases from zero to one, you move from the
 *       first point of the curve to the last point."
 *
 *       File Line Length: ~140
 */

using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Utility class with methods for getting a point on a quadratic Beziér curve; see Additional Comments header for more details
    /// </summary>
    public static class Bezier
    {
        /// <summary>
        /// Gets a point on a quadratic Bézier curve; see link for more details
        /// https://www.theappguruz.com/app/uploads/2015/07/bezier_quadratic.gif
        /// </summary>
        /// <param name="a">starting point</param>
        /// <param name="b">point to create curve from</param>
        /// <param name="c">ending point</param>
        /// <param name="t">interpolator</param>
        /// <param name="clamp">whether or not to clamp the interpolator</param>
        /// <returns>a quadratic Bézier point</returns>
        public static Vector3 GetQuadraticPoint(Vector3 a, Vector3 b, Vector3 c, float t, bool clamp = false)
        {
            // clamp the interpolator
            if (clamp) t = Mathf.Clamp(t, 0f, 1f);

            // inverts t such that 0 corresponds to a and 1 to c
            float r = 1f - t;

            // Bézier formula
            return r * r * a + 2f * r * t * b + t * t * c;
        }

        /// <summary>
        /// Gets a derivative point on a quadratic Bézier curve
        /// </summary>
        /// <param name="a">starting point</param>
        /// <param name="b">point to create curve from</param>
        /// <param name="c">ending point</param>
        /// <param name="t">interpolator</param>
        /// <param name="clamp">whether or not to clamp the interpolator</param>
        /// <returns>a quadratic Bézier derivative point</returns>
        public static Vector3 GetQuadraticDerivative(Vector3 a, Vector3 b, Vector3 c, float t, bool clamp = false)
        {
            // clamp the interpolator
            if (clamp) t = Mathf.Clamp(t, 0f, 0.99999f);

            // Bézier derivative formula
            return 2f * ((1f - t) * (b - a) + t * (c - b));
        }

        public static List<Vector3> ConstructQuadraticPath(List<Vector3> points, float stepSize)
        {
            if (points.Count <= 2) return new List<Vector3>(points);
            List<Vector3> path = new List<Vector3>();

            // Get fields
            float t = 0;
            Vector3 a, b, c;

            // Begin quadratic Bézier move
            c = points[0];
            for (int i = 1; i < points.Count; i++)
            {
                a = c;
                b = points[i - 1];
                c = (b + points[i]) * 0.5f;
                _March();
                t--;
            }

            // Final quadratic Bézier move
            a = c;
            b = points[^1];
            c = b;
            _March();

            path.Add(path[^1]);
            return path;

            // Constructions's local march function
            void _March()
            {
                // HACK: [BED-117] Bézier Distance Calculation: distance sub-optimal as march lurches at some points; but its okay fo now
                float distance = Vector3.Distance(a, b) + Vector3.Distance(b, c);
                float numberOfSteps = distance / stepSize;
                float increment = 1 / numberOfSteps;
                while (t < 1f)
                {
                    path.Add(GetQuadraticPoint(a, b, c, t));
                    t += increment;
                }
            }
        }
    }
}