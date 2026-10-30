/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 16, 2026
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
    public static class Vector2IntExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static Vector2 ToVector2(this Vector2Int vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector3 ToVector3(this Vector2Int vector)
        {
            return new Vector3(vector.x, vector.y);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        public static Vector2Int GetDirection(Vector2Int from, Vector2Int to)
        {
            return to - from;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}