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
    /// Class is for extension functionality regarding IList
    /// </summary>
    public static class IListExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Enums

        public enum ShuffleMethod
        {
            Naive,
            Fisher_Yates
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        private static System.Random rng = new System.Random();

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /// <summary>
        /// Randomizes IList with a Fisher-Yates shuffle; https://stackoverflow.com/a/1262619/11319808
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)  
        {  
            list.Shuffle(ShuffleMethod.Fisher_Yates);
        }

        public static void Shuffle<T>(this IList<T> list, ShuffleMethod shuffleMethod)  
        {  
            if (shuffleMethod == ShuffleMethod.Naive)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int k = UnityEngine.Random.Range(0, list.Count);
                    T value = list[k];  
                    list[k] = list[i];  
                    list[i] = value;  
                }
            }
            else
            {
                int n = list.Count;  
                while (n > 1) 
                {  
                    n--;  
                    int k = UnityEngine.Random.Range(0, n + 1);
                    T value = list[k];  
                    list[k] = list[n];  
                    list[n] = value;  
                }  
            }
        }

        public static T GetRandomElement<T>(this IList<T> list)
        {
            return list == null || list.Count == 0 ? default : list[Random.Range(0, list.Count)];
        }
        
        public static int GetRandomIndex<T>(this IList<T> list)
        {
            return list == null || list.Count == 0 ? -1 : Random.Range(0, list.Count);
        }
        
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        public static bool ContainsIndex<T>(this IList<T> list, int index)
        {
            if (list.Count == 0) return false;
            return -1 < index && index < list.Count;
        }

        public static int ClampIndex<T>(this IList<T> list, int index)
        {
            return index.Clamp(0, list.Count - 1);
        }

        public static string ToString<T>(this IList<T> list, bool formatElements, bool withNewlineCharacter = false)
        {
            if (formatElements)
            {
                string str = "[ ";
                for (int i = 0; i < list.Count - 1; i++)
                {
                    str += $"{list[i]}, ";
                    if (withNewlineCharacter) str += "\n";
                }
                if (list.Count != 0) str += $"{list[list.Count - 1]} ]";
                else str += "]";
                return str;
            }
            else
            {
                return list.ToString();
            }
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}