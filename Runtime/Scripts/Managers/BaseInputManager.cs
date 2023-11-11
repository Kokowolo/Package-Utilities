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
    public class BaseInputManager : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [Header("Singleton Settings")]
        [SerializeField] protected bool dontDestroyOnLoad = false;

        private bool isMouseOverUI = false;

        #endregion
        /************************************************************/
        #region Properties

        protected static BaseInputManager Instance => Singleton.Get<BaseInputManager>(findObjectOfType: true);

        #endregion
        /************************************************************/
        #region Functions

        protected virtual void Awake()
        {
            if (!Singleton.TrySet(this, dontDestroyOnLoad)) return;

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

        public static bool IsMouseOverUI() => Instance._IsMouseOverUI();
        public virtual bool _IsMouseOverUI()
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
            return Input.mousePosition;
//#endif
        }

        public static Vector3 GetMouseWorldPosition(LayerMask layerMask, 
            float maxDistance = Mathf.Infinity, Camera camera = null)
        {
            Raycasting.RaycastFromMouseScreenPoint(out RaycastHit raycastHit, layerMask, maxDistance, camera);
            return raycastHit.point;
        }

        public static bool WasClickPressedThisFrame() => Instance._WasClickPressedThisFrame();
        protected virtual bool _WasClickPressedThisFrame()
        {
#if ENABLE_INPUT_SYSTEM
            return Mouse.current.leftButton.wasPressedThisFrame;
#else
            return Input.GetMouseButtonDown(0);
#endif
        }
        
        #endregion
        /************************************************************/
    }
}