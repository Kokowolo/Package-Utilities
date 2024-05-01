/*
 * File Name: ColorUtils.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 23, 2023
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class ColorUtils
    {
        /************************************************************/
        #region Fields

        public static readonly Color lime = new Color(0.75f, 1, 0, 1);
        public static readonly Color orange = new Color(1, 0.75f, 0, 1);
        public static readonly Color pink = new Color(1, 0.75f, 0.75f, 1);

        #endregion
        /************************************************************/
        #region Functions

        public static Color GetColorHDR(float r, float g, float b, float intensity)
        {
            return GetColorHDR(r, g, b, 1f, intensity);
        }

        public static Color GetColorHDR(float r, float g, float b, float a, float intensity)
        {
            float factor = Mathf.Pow(2, intensity);
            return new Color(r * factor, g * factor, b * factor, a);
        }

        public static Color GetColorHDR(Color color, float intensity)
        {
            return GetColorHDR(color.r, color.g, color.b, color.a, intensity);
        }

        // public static Color TryParseHtmlString(string htmlString)
        // {
        //     ColorUtility.TryParseHtmlString(htmlString, out Color color);
        //     return color;
        // }
        
        #endregion
        /************************************************************/
    }
}