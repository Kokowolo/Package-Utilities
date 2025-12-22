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
    /// <summary>
    /// Class is for extension functionality regarding lists
    /// </summary>
    public static class ListExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions
        
        public static List<TOut> Cast<TIn, TOut>(this List<TIn> list) where TOut : TIn
        {
            return list.ConvertAll(value => (TOut) value);
        }

        // someList.Sort((a, b) => a.SomeInteger.CompareTo(b.SomeInteger));

        #endregion
        /*——————————————————————————————————————————————————————————*/
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}