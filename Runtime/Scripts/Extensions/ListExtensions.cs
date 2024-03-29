/*
 * File Name: StringExtensions.cs
 * Description: This script is for extension functionality regarding strings
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
        
        public static void Swap<T>(this List<T> list, int indexA, int indexB)
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        public static string ToString<T>(this List<T> list, bool formatElements, bool withNewlineCharacter = false)
        {
            if (formatElements)
            {
                string str = "[ ";
                for (int i = 0; i < list.Count - 1; i++)
                {
                    str += $"{list[i].ToString()}, ";
                    if (withNewlineCharacter) str += "\n";
                }
                if (list.Count != 0) str += $"{list[list.Count - 1].ToString()} ]";
                else str += "]";
                return str;
            }
            else
            {
                return list.ToString();
            }
        }

        public static List<TOut> Cast<TIn, TOut>(this List<TIn> list) where TOut : TIn
        {
            return list.ConvertAll(value => (TOut) value);
        }

        // someList.Sort((a, b) => a.SomeInteger.CompareTo(b.SomeInteger));
        
        #endregion
        /************************************************************/
    }
}