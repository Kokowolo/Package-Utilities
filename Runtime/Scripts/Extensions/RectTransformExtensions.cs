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

        // // convert the screen position to the local anchored position
        // public static Vector2 ToAnchoredPoint(this RectTransform rectTransform, Vector3 worldPositon)
        // {
        //     ScreenUtils.WorldToScreenPoint(Camera.main, worldPosition);
        //     Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint); 
        // }

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

        public static Vector2 ToScreenPoint01(this RectTransform rectTransform)
        {
            return ScreenUtils.ToScreenPoint01(rectTransform.position);
        }

        #endregion
        /************************************************************/
    }
}