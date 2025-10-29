/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 23, 2023
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class ColorExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Color Intensify(this Color color, float intensity)
        {
            return ColorUtils.GetColorHDR(color.r, color.g, color.b, color.a, intensity);
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static Color ToNegative(this Color color)
        {
            Color.RGBToHSV(color, out float H, out float S, out float V);
            float negativeH = (H + 0.5f) % 1f;
            return Color.HSVToRGB(negativeH, S, V);
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
        /*██████████████████████████████████████████████████████████*/
    }
}