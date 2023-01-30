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
        
        #endregion
        /************************************************************/
    }
}