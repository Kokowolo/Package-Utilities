/*
 * File Name: WorldSpaceDisplay.cs
 * Description: This script is for handling a Text Display Prefab in world space
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 30, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Kokowolo.Utilities
{
    [RequireComponent(typeof(LineRenderer))]
    public class WorldSpaceDisplay : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [Header("Cached References")]
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
        /************************************************************/
        #region Fields

        LineRenderer LineRenderer => this.CacheGetComponent<LineRenderer>(_lineRenderer);

        #endregion
        /************************************************************/
        #region Functions

        private void Awake()
        {
            position = transform.position;
            LineRenderer.positionCount = 2;
            Refresh();
        }

        private void Update()
        {
            Refresh();
            SetPosition();
        }

        private void Refresh()
        {
            LineRenderer.SetPosition(0, target.position);
            LineRenderer.SetPosition(1, lineRendererEnd.position);
        }

        private void SetPosition()
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
        /************************************************************/
        #region Debug
        #if UNITY_EDITOR

        private void OnDrawGizmosSelected() 
        {
            if (!target) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(target.position, radius);
        }

        #endif
        #endregion
        /************************************************************/
    }
}

