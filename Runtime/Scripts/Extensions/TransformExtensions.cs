/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class TransformExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static void SetLossyScale(this Transform transform, Vector3 scale)
        {
            transform.localScale = Vector3.one;
            var m = transform.worldToLocalMatrix;
            m.SetColumn(0, new Vector4(m.GetColumn(0).magnitude, 0f));
            m.SetColumn(1, new Vector4(0f, m.GetColumn(1).magnitude));
            m.SetColumn(2, new Vector4(0f, 0f, m.GetColumn(2).magnitude));
            m.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
            transform.localScale = m.MultiplyPoint(scale);
        }

        // public static void SetLossyScale(this Transform transform, Vector3 lossyScale)
        // {
        //     Transform parent = transform.parent;
        //     transform.SetParent(null);
        //     transform.localScale = lossyScale;
        //     transform.SetParent(parent);
        // }

        /// <summary>
        /// Gets first Component of the matching type in hierarchy, otherwise null if no Component is found.
        /// </summary>
        public static T GetComponentInHierarchy<T>(this Transform transform) where T : Component
        {
            T component = null;
            while (!component && transform.parent != null)
            {
                transform = transform.parent;
                component = transform.GetComponent<T>();
            }
            return component;
        }

        public static Transform GetHierarchyRoot(this Transform transform)
        {
            while (transform.parent != null)
            {
                transform = transform.parent;
            }
            return transform;
        }

        public static Transform GetLastChild(this Transform transform)
        {
            int childCount = transform.childCount;
            if (childCount == 0) return null;
            return transform.GetChild(childCount - 1);
        }

        public static void DestroyImmediateChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Recursively searches children/grandchildren to find a child by name n and return it
        /// </summary>
        /// <returns>The found child transform; Null if child with matching name isn't found</returns>
        public static Transform FindRecursively(this Transform transform, string n)
        {
            foreach (Transform child in transform)
            {
                if (child.name == n) return child;
                Transform grandchild = child.FindRecursively(n);
                if (grandchild) return grandchild;
            }
            return null;
        }

        /// <summary>
        /// Transforms a rotation from local space to world space.
        /// </summary>
        public static Quaternion TransformRotation(this Transform transform, Quaternion rotation)
        {
            return transform.rotation * rotation;
        }

        /// <summary>
        /// Transforms a rotation from world space to local space. The opposite of Transform.TransformRotation.
        /// </summary>
        public static Quaternion InverseTransformRotation(this Transform transform, Quaternion rotation)
        {
            return Quaternion.Inverse(transform.rotation) * rotation;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}