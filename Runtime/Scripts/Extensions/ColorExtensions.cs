/*
 * File Name: ColorExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 23, 2023
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class ColorExtensions
    {
        /************************************************************/
        #region Functions

        public static Color Intensify(this Color color, float intensity)
        {
            return ColorUtils.GetColorHDR(color.r, color.g, color.b, color.a, intensity);
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        // public static string ToHtmlStringRGB(this Color color)
        // {
        //     return ColorUtility.ToHtmlStringRGB(color);
        // }

        // public static string ToHtmlStringRGBA(this Color color)
        // {
        //     return ColorUtility.ToHtmlStringRGBA(color);
        // }
        
        #endregion
        /************************************************************/
    }
}