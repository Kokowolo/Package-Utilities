/*
 * File Name: TransformExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
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

        public static Transform GetLastChild(this Transform transform)
        {
            int childCount = transform.childCount;
            if (childCount == 0) return null;
            return transform.GetChild(childCount - 1);
        }
        
        #endregion
        /************************************************************/
    }
}