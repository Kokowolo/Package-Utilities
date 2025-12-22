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
        #region Fields
        
        // NOTE: this is now available in Unity 6.2 and greater
        public static readonly Color forestGreen = new Color(0.003921569f, 0.2666667f, 0.1294118f, 1f);
        public static readonly Color lime = new Color(0.75f, 1, 0, 1);
        public static readonly Color orange = new Color(1, 0.75f, 0, 1);
        public static readonly Color pink = new Color(1, 0.75f, 0.75f, 1);
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static Color Intensify(this Color color, float intensity)
        {
            return GetColorHDR(color.r, color.g, color.b, color.a, intensity);
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
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

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
        /*——————————————————————————————————————————————————————————*/
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}