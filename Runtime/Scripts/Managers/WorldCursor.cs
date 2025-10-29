/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 29, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class WorldCursor : MonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Events

        public event EventHandler OnHitInfoTransformChanged;
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Header("References")]
        [SerializeField] GameObject visual;

        [Header("Settings")]
        public LayerMask layerMask;

        Transform previousTransform;
        RaycastHit hitInfo;
        bool doRaycast = true;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public RaycastHit HitInfo => hitInfo;
        public Transform HitInfoTransform => hitInfo.transform;
        public Vector3 Position => hitInfo.point;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        // void Awake()
        // {
        //     if (WorldCursorManager.Cursor == null)
        //     {
        //         WorldCursorManager.Cursor = this;
        //     }
        // }

        // void OnDestroy()
        // {
        //     if (WorldCursorManager.Cursor == this)
        //     {
        //         WorldCursorManager.Cursor = null;
        //     }
        // }
        
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

        public void SetVisual(bool isActive, bool hasCollider)
        {
            visual.SetActive(isActive);
            if (visual.TryGetComponent(out Collider collider))
            {
                collider.enabled = hasCollider;
            }
            else
            {
                LogManager.LogWarning("could not find Collider component on visual");
            }
        }

        public void SetCursorWorldPosition(Vector3 worldPosition)
        {
            InputManager.SetCursorWorldPosition(worldPosition);

            hitInfo.point = worldPosition;
            if (visual)
            {
                visual.transform.position = worldPosition;
            }
            doRaycast = false;
        }

        public new bool TryGetComponent<T>(out T component) where T : Component
        {
            if (HitInfoTransform) return HitInfo.transform.TryGetComponent(out component);
            component = null;
            return false;
        }

        public new T GetComponent<T>() where T : Component
        {
            if (HitInfoTransform) return HitInfo.transform.GetComponent<T>();
            return null;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}