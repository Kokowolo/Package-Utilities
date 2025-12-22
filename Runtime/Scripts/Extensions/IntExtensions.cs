/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 25, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class IntExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static int Abs(this int value)
        {
            return Mathf.Abs(value);
        }

        public static int Clamp(this int value, int min, int max)
        {
            return Mathf.Clamp(value, min, max);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}