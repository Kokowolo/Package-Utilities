/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 28, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class ScreenUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Vector2 WorldToScreenPoint(Vector3 worldPosition)
        {
            return WorldToScreenPoint(Camera.main, worldPosition);
        }
        
        public static Vector2 WorldToScreenPoint(Camera camera, Vector3 worldPosition)
        {
            return RectTransformUtility.WorldToScreenPoint(camera, worldPosition);
        }

        public static Vector3 ScreenPointToWorld(Vector3 screenPoint)
        {
            return ScreenPointToWorld(Camera.main, screenPoint);
        }

        public static Vector3 ScreenPointToWorld(Camera camera, Vector3 screenPoint)
        {
            return camera.ScreenToWorldPoint(screenPoint);
        }

        public static Vector3 GetMouseWorldPoint(Camera camera, float depth)
        {
            Vector3 screenPoint = InputManager.GetMouseScreenPoint();
            screenPoint.z = depth;
            return ScreenPointToWorld(camera, screenPoint);
        }

        public static Vector3 GetMouseWorldPoint(Camera camera)
        {
            return GetMouseWorldPoint(camera, camera.nearClipPlane);
        }

        public static Vector3 GetMouseWorldPoint(float depth)
        {
            return GetMouseWorldPoint(Camera.main, depth);
        }

        public static Vector3 GetMouseWorldPoint()
        {
            return GetMouseWorldPoint(Camera.main);
        }
        
        public static Vector2 GetMouseScreenPoint()
        {
            return InputManager.GetMouseScreenPoint();
        }

        public static Vector2 GetMouseScreenPointNormalized()
        {
            return ToScreenPointNormalized(InputManager.GetMouseScreenPoint());
        }

        public static Vector2 ToScreenPointNormalized(Vector2 screenPoint)
        {
            return new Vector2()
            {
                x = Mathf.Clamp(screenPoint.x, 0, Screen.width) / Screen.width,
                y = Mathf.Clamp(screenPoint.y, 0, Screen.height) / Screen.height
            };
        }

        public static Vector2 ToScreenPoint(Vector2 screenPointNormalized)
        {
            return new Vector2()
            {
                x = Mathf.Clamp01(screenPointNormalized.x) * Screen.width,
                y = Mathf.Clamp01(screenPointNormalized.y) * Screen.height
            };
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}