/*
 * File Name: IListExtensions.cs
 * Description: This script is for extension functionality regarding IList
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
    public static class IListExtensions
    {
        /************************************************************/
        #region Fields

        private static System.Random rng = new System.Random();

        #endregion
        /************************************************************/
        #region Functions

        /// <summary>
        /// randomizes IList with a Fisher-Yates shuffle; https://stackoverflow.com/a/1262619/11319808
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)  
        {  
            Shuffle(list, rng);
        }

        /// <summary>
        /// randomizes IList with a Fisher-Yates shuffle; https://stackoverflow.com/a/1262619/11319808
        /// </summary>
        public static void Shuffle<T>(this IList<T> list, System.Random rng)  
        {  
            int n = list.Count;  
            while (n > 1) 
            {  
                n--;  
                int k = rng.Next(n + 1);
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
        
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        public static string ToString<T>(this IList<T> list, bool formatElements, bool withNewlineCharacter = false)
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
        
        #endregion
        /************************************************************/
    }
}