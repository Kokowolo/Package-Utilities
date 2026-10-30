/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 30, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Handles a Text Display Prefab in world space
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class WorldSpaceDisplay : MonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Header("References")]
        [SerializeField] TMP_Text text = null;
        [SerializeField] Transform lineRendererEnd = null;

        [Header("Settings")]
        [SerializeField] Transform target;
        [SerializeField, Min(0)] float radius = 3;
        [SerializeField, Min(0)] float speed;
        [SerializeField] bool lockHeight = true;

        Vector3 position;
        LineRenderer _lineRenderer;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        LineRenderer LineRenderer => this.CacheGetComponent<LineRenderer>(ref _lineRenderer);

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        void Awake()
        {
            position = transform.position;
            LineRenderer.positionCount = 2;
            Refresh();
        }

        void Update()
        {
            Refresh();
            SetPosition();
        }

        void Refresh()
        {
            LineRenderer.SetPosition(0, target.position);
            LineRenderer.SetPosition(1, lineRendererEnd.position);
        }

        void SetPosition()
        {
            position = (position - target.position).normalized * radius;
            position += target.position;
            if (lockHeight) position.y = transform.position.y;
            position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
            transform.position = position;
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Editor
#if UNITY_EDITOR

        void OnDrawGizmosSelected() 
        {
            if (!target) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(target.position, radius);
        }

#endif
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}

