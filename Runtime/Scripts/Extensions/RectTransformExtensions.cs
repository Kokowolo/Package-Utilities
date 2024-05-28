/*
 * File Name: RectTransformExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 28, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class RectTransformExtensions
    {
        /************************************************************/
        #region Functions

        // public static Vector2 ToScreenPoint(this RectTransform rectTransform)
        // {
        //     return rectTransform.position;
        // }

        public static Vector2 ToScreenPoint01(this RectTransform rectTransform)
        {
            return ScreenUtils.ToScreenPoint01(rectTransform.position);
        }

        #endregion
        /************************************************************/
    }
}