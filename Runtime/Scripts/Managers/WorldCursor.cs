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
        [SerializeField] protected GameObject visual;

        [Header("Settings")]
        [SerializeField] public LayerMask layerMask;

        protected Transform previousTransform;
        protected RaycastHit hitInfo;
        bool doRaycast = true;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public RaycastHit HitInfo => hitInfo;
        public Transform HitInfoTransform => hitInfo.transform;
        public Vector3 Position => hitInfo.point;

        public GameObject Visual => visual;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions
        
        protected virtual void Update() // NOTE: WorldCursor should execute before default execution order if order matters
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

        public void SetActiveVisual(bool isActive, bool hasCollider)
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

        public bool TryGetComponentInHit<T>(out T component) where T : Component
        {
            if (HitInfoTransform) return HitInfo.transform.TryGetComponent(out component);
            component = null;
            return false;
        }

        public T GetComponentInHit<T>() where T : Component
        {
            if (HitInfoTransform) return HitInfo.transform.GetComponent<T>();
            return null;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}