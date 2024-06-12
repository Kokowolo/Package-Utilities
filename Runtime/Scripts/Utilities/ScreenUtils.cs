/*
 * File Name: ScreenUtils.cs
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
    public static class ScreenUtils
    {
        /************************************************************/
        #region Functions

        public static Vector2 WorldToScreenPoint(Vector3 worldPosition)
        {
            return WorldToScreenPoint(Camera.main, worldPosition);
        }
        
        public static Vector2 WorldToScreenPoint(Camera camera, Vector3 worldPosition)
        {
            return RectTransformUtility.WorldToScreenPoint(camera, worldPosition);
        }

        public static Vector2 GetMouseScreenPoint01()
        {
            return ToScreenPoint01(BaseInputManager.GetMouseScreenPoint());
        }

        public static Vector2 ToScreenPoint01(Vector2 screenPoint)
        {
            return new Vector2()
            {
                x = Mathf.Clamp(screenPoint.x, 0, Screen.width) / Screen.width,
                y = Mathf.Clamp(screenPoint.y, 0, Screen.height) / Screen.height
            };
        }

        #endregion
        /************************************************************/
    }
}