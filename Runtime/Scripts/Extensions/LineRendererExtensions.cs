/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 13, 2023
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class LineRendererExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static void SetPositions(this LineRenderer lineRenderer, List<Vector3> positions)
        {
            lineRenderer.positionCount = positions.Count;
            for (int i = 0; i < positions.Count; i++)
            {
                lineRenderer.SetPosition(i, positions[i]);
            }
        }

        // public static Vector3[] AddQuadraticBezier(this Vector3[] points, int resolution)
        // {
        //     if (resolution < 1)
        //     {
        //         Debug.LogError("AddQuadraticBezier resolution parameter must be greater than 0");
        //         return null;
        //     }

        //     Vector3[] bezierPoints = new Vector3[points.Length + resolution * (points.Length - 1)];
        //     bezierPoints[0] = points[0];
        //     // bezierPoints[bezierPoints.Length - 1] = points[points.Length - 1];

        //     Vector3 a, b, c;
        //     float t = 0;

        //     for (int i = 0; i < points.Length - 1; i++)
        //     {
        //         int index = i * (resolution + 1);
        //         if (i == 0)
        //         {
        //             a = points[0];
        //             b = points[0];
        //             c = (points[0] + points[1]) * 0.5f;
        //         }
        //         else if (i == points.Length - 1)
        //         {
        //             a = (points[i - 1] + points[i]) * 0.5f;
        //             b = points[i];
        //             c = points[i];
        //         }
        //         else
        //         {
        //             a = (points[i - 1] + points[i]) * 0.5f;
        //             b = points[i];
        //             c = (points[i] + points[i + 1]) * 0.5f;
        //         }

        //         for (int j = 0; j < resolution; j++)
        //         {
        //             t = (float)j / resolution;
        //             bezierPoints[index + j] = Bezier.GetQuadraticPoint(a, b, c, t);
        //         }
        //     }
        //     return bezierPoints;
        // }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}