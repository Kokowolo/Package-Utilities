/*
 * File Name: CursorManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: August 22, 2022
 * 
 * Additional Comments:
 *      File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.InputSystem;
using Kokowolo.Utilities;

[DefaultExecutionOrder(-100)]
public class CursorManager : MonoSingleton<CursorManager>
{
    /************************************************************/
    #region Events

    public static event EventHandler OnHitInfoTransformChanged;
    // public class OnHitInfoChangedEventArgs
    // {
    //     public bool IsValid => HasValidHitInfo;
    // }
    
    #endregion
    /************************************************************/
    #region Fields

    // private OnHitInfoChangedEventArgs args = new OnHitInfoChangedEventArgs();

    [Header("Cached References")]
    [SerializeField] private GameObject cursorVisual;

    [Header("Settings")]
    [Tooltip("layerMask the manager can interact with")]
    [SerializeField] private LayerMask layerMask;

    private RaycastHit hitInfo;
    private Transform previousTransform;

    #endregion
    /************************************************************/
    #region Properties

    public static LayerMask LayerMask 
    { 
        get => Instance.layerMask; 
        set => Instance.layerMask = value; 
    }

    public static RaycastHit HitInfo => Instance.hitInfo;

    public static bool HasValidHitInfo => HitInfo.transform != null;

    static bool doRaycast = true;

    #endregion
    /************************************************************/
    #region Functions

    private void LateUpdate()
    {
        DoRaycast();
        doRaycast = true;
    }

    private void DoRaycast()
    {
        if (!doRaycast) return;
        
        Raycasting.RaycastFromMouseScreenPoint(out hitInfo, layerMask);
        cursorVisual.transform.position = hitInfo.point;

        if (hitInfo.transform != previousTransform)
        {
            OnHitInfoTransformChanged?.Invoke(this, EventArgs.Empty);
            previousTransform = hitInfo.transform;
        }
    }

    public static void SetActiveCursorVisual(bool value)
    {
        Instance.cursorVisual.SetActive(value);
    }

    public static Vector3 GetWorldPosition()
    {
        return Instance.hitInfo.point;
    }

    public static void SetCursorPosition(Vector3 worldPosition)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
        Mouse.current.WarpCursorPosition(screenPoint);

        Instance.hitInfo.point = worldPosition;
        Instance.cursorVisual.transform.position = worldPosition;
        doRaycast = false;
    }

    #endregion
    /************************************************************/
}