/*
 * File Name: TransformExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *		File Line Length: 420
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class TransformExtensions
    {
        /************************************************************/
        #region Functions

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

        public static Transform GetGrandparent(this Transform transform)
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
        
        #endregion
        /************************************************************/
    }
}