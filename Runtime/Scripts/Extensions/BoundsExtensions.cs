/*
 * File Name: BoundsExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 23, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class BoundsExtensions
    {
        /************************************************************/
        #region Properties

        #endregion
        /************************************************************/
        #region Functions

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
        /************************************************************/
    }
}