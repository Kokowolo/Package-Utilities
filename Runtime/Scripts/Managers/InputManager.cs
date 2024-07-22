/*
 * File Name: InputManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 7, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Kokowolo.Utilities
{
    [DefaultExecutionOrder(-100)]
    public class InputManager : MonoSingleton<InputManager>
    {
        /************************************************************/
        #region Fields

        private bool isMouseOverUI = false;

        #endregion
        /************************************************************/
        #region Functions

        protected override void MonoSingleton_Awake()
        {
#if ENABLE_INPUT_SYSTEM
            enabled = true;
#else
            enabled = false;
#endif
        }

        protected virtual void Update()
        {
            isMouseOverUI = EventSystem.current.IsPointerOverGameObject();
        }

        public static bool IsMouseOverUI() => Instance.InputManager_IsMouseOverUI();
        protected virtual bool InputManager_IsMouseOverUI()
        {
#if ENABLE_INPUT_SYSTEM
            return isMouseOverUI;
#else
            return EventSystem.current.IsPointerOverGameObject();
#endif
        }

        public static Vector2 GetMouseScreenPoint()
        {
//#if ENABLE_INPUT_SYSTEM
//            return Mouse.current.position.ReadValue();
//#else
            // NOTE: using UnityEngine.Input over InputSystem due to bug within `Mouse.current.WarpCursorPosition(Vector2)`
            return Input.mousePosition; 
//#endif
        }

        public static void SetCursorWorldPosition(Vector3 worldPosition)
        {
#if ENABLE_INPUT_SYSTEM
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
            SetCursorScreenPosition(screenPoint);
#else
            throw new System.NotImplementedException();
#endif
        }

        public static void SetCursorScreenPosition(Vector2 screenPoint)
        {
#if ENABLE_INPUT_SYSTEM
            Mouse.current.WarpCursorPosition(screenPoint);
#else
            throw new System.NotImplementedException();
#endif
        }
        
        #endregion
        /************************************************************/
    }
}