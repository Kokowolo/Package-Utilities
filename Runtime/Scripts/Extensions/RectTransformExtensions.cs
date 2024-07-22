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

        // public static float GetWidth(this RectTransform rectTransform)
        // {
        //     return rectTransform.rect.width;
        // }

        // public static float GetHeight(this RectTransform rectTransform)
        // {
        //     return rectTransform.rect.height;
        // }

        // public static Vector2 ToScreenPoint(this RectTransform rectTransform)
        // {
        //     return rectTransform.position;
        // }

        // public static Vector2 ToAnchoredPosition(this RectTransform rectTransform, Vector2 screenPoint)
        // {
        //     return rectTransform.InverseTransformPoint(screenPoint); 
        // }

        public static Vector2 GetScreenPointNormalized(this RectTransform rectTransform)
        {
            return ScreenUtils.ToScreenPointNormalized(rectTransform.position);
        }

        #endregion
        /************************************************************/
    }
}