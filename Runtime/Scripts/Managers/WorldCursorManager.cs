/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: August 22, 2022
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.InputSystem;
using Kokowolo.Utilities;

[DefaultExecutionOrder(-100)]
public class WorldCursorManager : MonoBehaviourSingleton<WorldCursorManager>
{
    /*██████████████████████████████████████████████████████████*/
    #region Events

    public static event EventHandler OnHitInfoTransformChanged;
    // public class OnHitInfoChangedEventArgs
    // {
    //     public bool IsValid => HasValidHitInfo;
    // }
    
    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Fields

    // OnHitInfoChangedEventArgs args = new OnHitInfoChangedEventArgs();

    [Header("Cached References")]
    [SerializeField] GameObject visual;

    [Header("Settings")]
    [Tooltip("layerMask the manager can interact with")]
    [SerializeField] LayerMask layerMask;

    RaycastHit hitInfo;
    Transform previousTransform;

    #endregion
    /*██████████████████████████████████████████████████████████*/
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
    /*██████████████████████████████████████████████████████████*/
    #region Functions

    void LateUpdate()
    {
        DoRaycast();
        doRaycast = true;
    }

    void DoRaycast()
    {
        if (!doRaycast) return;
        
        Raycasting.RaycastFromMouseScreenPoint(out hitInfo, layerMask);
        if (visual)
        {
            visual.transform.position = hitInfo.point;
        }

        if (hitInfo.transform != previousTransform)
        {
            OnHitInfoTransformChanged?.Invoke(this, EventArgs.Empty);
            previousTransform = hitInfo.transform;
        }
    }

    public static void SetVisual(bool isActive, bool hasCollider)
    {
        Instance.visual.SetActive(isActive);
        Instance.visual.GetComponent<Collider>().enabled = hasCollider;
    }

    public static Vector3 GetWorldPosition()
    {
        return Instance.hitInfo.point;
    }

    public static void SetCursorWorldPosition(Vector3 worldPosition)
    {
        InputManager.SetCursorWorldPosition(worldPosition);

        Instance.hitInfo.point = worldPosition;
        if (Instance.visual)
        {
            Instance.visual.transform.position = worldPosition;
        }
        doRaycast = false;
    }

    public static Vector3 GetMouseWorldPosition(LayerMask layerMask, float maxDistance = Mathf.Infinity, Camera camera = null)
    {
        GetMouseWorldPosition(out RaycastHit hitInfo, layerMask, maxDistance, camera);
        return hitInfo.point;
    }

    public static Vector3 GetMouseWorldPosition(out RaycastHit hitInfo, LayerMask layerMask, 
        float maxDistance = Mathf.Infinity, Camera camera = null)
    {
        Raycasting.RaycastFromMouseScreenPoint(out hitInfo, layerMask, maxDistance, camera);
        return hitInfo.point;
    }

    #endregion
    /*██████████████████████████████████████████████████████████*/
}