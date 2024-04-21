/*
 * File Name: ListExtensions.cs
 * Description: This script is for extension functionality regarding lists
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *      File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class ListExtensions
    {
        /************************************************************/
        #region Functions
        
        public static List<TOut> Cast<TIn, TOut>(this List<TIn> list) where TOut : TIn
        {
            return list.ConvertAll(value => (TOut) value);
        }

        // someList.Sort((a, b) => a.SomeInteger.CompareTo(b.SomeInteger));
        
        #endregion
        /************************************************************/
    }
}