/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 16, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsIntExtensions
{
    /*██████████████████████████████████████████████████████████*/
    #region Properties

    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Functions

    /// <summary>
    /// Grows the Bounds to include the point. (copied from Bounds.cs)
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="point"></param>
    public static void Encapsulate(this ref BoundsInt bounds, Vector3Int point)
    {
        bounds.SetMinMax(Vector3Int.Min(bounds.min, point), Vector3Int.Max(bounds.max, point));
    }

    /// <summary>
    /// Grow the bounds to encapsulate the bounds. (copied from Bounds.cs)
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="otherBounds"></param>
    public static void Encapsulate(this ref BoundsInt bounds, BoundsInt otherBounds)
    {
        bounds.Encapsulate(otherBounds.min); // Encapsulate(otherBounds.center - otherBounds.extents); // min
        bounds.Encapsulate(otherBounds.max); // Encapsulate(otherBounds.center + otherBounds.extents); // max
    }

    #endregion
    /*██████████████████████████████████████████████████████████*/
}