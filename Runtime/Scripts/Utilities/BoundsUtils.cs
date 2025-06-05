/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: November 5, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class BoundsUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

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
        /*██████████████████████████████████████████████████████████*/
    }
}