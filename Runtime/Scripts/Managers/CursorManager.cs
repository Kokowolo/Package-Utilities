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

public class CursorManager : MonoBehaviour
{
    /************************************************************/
    #region Fields

    [Header("Singleton Settings")]
        [SerializeField] protected bool dontDestroyOnLoad = false;

    [Header("Cached References")]
    [SerializeField] private GameObject cursorVisual;

    [Header("Settings")]
    [Tooltip("layerMask the manager can interact with")]
    [SerializeField] private LayerMask layerMask;

    private RaycastHit hitInfo;

    #endregion
    /************************************************************/
    #region Properties

    private static CursorManager Instance => Singleton.Get<CursorManager>();

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

    private void Awake() 
    {
        Singleton.TrySet(this, dontDestroyOnLoad: dontDestroyOnLoad);
    }

    private void Update() 
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