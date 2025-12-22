/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 23, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class BoundsExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static BoundsInt ToBoundsInt(this Bounds bounds, float resolution = 1)
        {
            Vector3Int position = new Vector3Int
            {
                x = Mathf.RoundToInt(bounds.min.x / resolution),
                y = Mathf.RoundToInt(bounds.min.y / resolution),
                z = Mathf.RoundToInt(bounds.min.z / resolution),
            };
            Vector3Int size = new Vector3Int
            {
                x = Mathf.RoundToInt(bounds.size.x / resolution),
                y = Mathf.RoundToInt(bounds.size.y / resolution),
                z = Mathf.RoundToInt(bounds.size.z / resolution),
            };
            return new BoundsInt(position, size);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        public static Bounds CreateBounds(float xMin, float yMin, float zMin, float sizeX, float sizeY, float sizeZ)
        {
            return CreateBounds(new Vector3(xMin, yMin, zMin), new Vector3(sizeX, sizeY, sizeZ));
        }

        public static Bounds CreateBounds(Vector3 minPosition, Vector3 size)
        {
            Vector3 center = new Vector3(
                minPosition.x + size.x * 0.5f, 
                minPosition.y + size.y * 0.5f, 
                minPosition.z + size.z * 0.5f
            );
            return new Bounds(center, size);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}