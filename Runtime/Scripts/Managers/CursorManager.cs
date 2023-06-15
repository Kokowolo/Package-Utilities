/*
 * File Name: CursorManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: August 22, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kokowolo.Utilities;

[DefaultExecutionOrder(-100)]
public class CursorManager : MonoSingleton<CursorManager>
{
    /************************************************************/
    #region Fields

    [Header("Cached References")]
    [SerializeField] private GameObject cursorVisual;

    [Header("Settings")]
    [Tooltip("layerMask the manager can interact with")]
    [SerializeField] private LayerMask layerMask;

    private RaycastHit hitInfo;

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

    #endregion
    /************************************************************/
    #region Functions

    private void LateUpdate() 
    {
        Raycasting.RaycastFromMouseScreenPoint(out hitInfo, layerMask);
        cursorVisual.transform.position = hitInfo.point;
    }

    public static void SetActiveCursorVisual(bool value)
    {
        Instance.cursorVisual.SetActive(value);
    }

    public static Vector3 GetWorldPosition()
    {
        return Instance.hitInfo.point;
    }

    #endregion
    /************************************************************/
}