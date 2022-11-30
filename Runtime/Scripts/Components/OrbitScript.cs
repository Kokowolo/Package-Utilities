/*
 * File Name: OrbitScript.cs
 * Description: This script is for orbiting a GameObject around a target transform
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 3, 2022
 * 
 * Additional Comments:
 *      While this file has been updated to better fit this project, the original version can be found here:
 *		https://youtu.be/hd1QzLf4ZH8
 *
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class OrbitScript : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [Header("Cached References")]
        [Tooltip("transform to orbit around")]
        [SerializeField] public Transform target = null;

        [Header("Settings")]
        [SerializeField, Min(0)] public float radius = 1;
        [SerializeField, Range(0, 10)] public float speed = 1;
        [SerializeField] private bool lockHeight = true;

        public float t = 0; // where the object is relative to 2π

        #endregion
        /************************************************************/
        #region Properties

        // public float Radius { get => _radius; set => _radius = value; }

        #endregion
        /************************************************************/
        #region Functions

        private void LateUpdate()
        {
            PositionalOrbit();
        }

        private void PositionalOrbit()
        {
            t += Time.deltaTime * speed;
            if (t > 24 * Mathf.PI) t -= 24 * Mathf.PI;
            Vector2 xy = GetPoint(t);
            Vector3 position = target.position + new Vector3(xy.x, 0, xy.y);
            position = Vector3.Slerp(transform.position, position, Time.deltaTime * speed);
            if (lockHeight) position.y = transform.position.y;
            transform.position = position;
        }

        private Vector2 GetPoint(float radians)
        {
            return new Vector2(radius * Mathf.Sin(radians), radius * Mathf.Cos(radians));
        }

        // NOTE: this does not work as intended
        // private void RotationalOrbit()
        // {
        //     Vector3 relativePos = (target.position + new Vector3(0, 0.5f, 0)) - transform.position;
        //     Quaternion rotation = Quaternion.LookRotation(relativePos);

        //     Quaternion current = transform.localRotation;

        //     transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        //     transform.Translate(0, 0, speed * Time.deltaTime);
        // }

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