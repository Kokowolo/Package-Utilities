/*
 * File Name: FloatExtensions.cs
 * Description: This script is for extension functionality regarding floats
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class FloatExtensions
    {
        /************************************************************/
        #region Functions

        public static float Remap(this float value, Vector2 from, Vector2 to)
        {
            return MathKoko.Remap(value, from, to);
        }

        public static float Remap(this float value, float from1, float from2, float to1, float to2)
        {
            return MathKoko.Remap(value, from1, from2, to1, to2);
        }

        public static float RemapFrom01(this float value, Vector2 to)
        {
            return MathKoko.RemapFrom01(value, to);
        }

        public static float RemapFrom01(this float value, float to1, float to2)
        {
            return MathKoko.RemapFrom01(value, to1, to2);
        }

        public static float RemapTo01(this float value, Vector2 from)
        {
            return MathKoko.RemapTo01(value, from);
        }

        public static float RemapTo01(this float value, float from1, float from2)
        {
            return MathKoko.RemapFrom01(value, from1, from2);
        }
        
        // public static string ToStringDegrees(this float value)
        // {
        //     return $"{value.ToString()}°";
        // }

        // public static string ToStringDecimal(this float value, int decimalPlaces)
        // {
        //     // check out https://stackoverflow.com/a/58733847/11319808 for optional decimal places
        //     return isOptional ? $"{value:G3}
        // }
        
        #endregion
        /************************************************************/
    }
}