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

using Kokowolo.Utilities;

#if ENABLE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM_PACKAGE
#define USE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[DefaultExecutionOrder(-100)]
public class InputManager : MonoBehaviour
{
	/************************************************************/
    #region Properties

    protected static InputManager Instance => Singleton<InputManager>.Get();

    #endregion
    /************************************************************/
    #region Functions

    protected virtual void Awake()
    {
        Singleton<InputManager>.Set(this);
    }

    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static Vector2 GetMouseScreenPoint()
    {
#if USE_NEW_INPUT_SYSTEM
        return Mouse.current.position.ReadValue();
#else
        return Input.mousePosition;
#endif
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
#if USE_NEW_INPUT_SYSTEM
        return Mouse.current.leftButton.wasPressedThisFrame;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }
    
    #endregion
    /************************************************************/
}