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
        
        #endregion
        /************************************************************/
    }
}