/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 7, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
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
    public class InputManager : InputManagerBase<InputManager> {}

    [DefaultExecutionOrder(-100)]
    public abstract class InputManagerBase<T> : MonoBehaviourSingleton<T> where T : InputManagerBase<T>
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        bool isPointerOverUI = false;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        protected override void Singleton_Awake()
        {
#if ENABLE_INPUT_SYSTEM
            enabled = true;
#else
            enabled = false;
#endif
        }

        protected virtual void Update()
        {
            isPointerOverUI = EventSystem.current.IsPointerOverGameObject();
        }

        public static bool IsPointerOverUI() => Instance.InputManager_IsPointerOverUI();
        protected virtual bool InputManager_IsPointerOverUI()
        {
#if ENABLE_INPUT_SYSTEM
            return isPointerOverUI;
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
        /*██████████████████████████████████████████████████████████*/
    }
}