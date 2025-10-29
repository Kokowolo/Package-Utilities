/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Authors: Catlike Coding, Will Lacey
 * Date Created: September 24, 2020
 * 
 * Additional Comments: 
 *      The original version of this file can be found here: https://catlikecoding.com/unity/tutorials/hex-map/ within 
 *       Catlike Coding's tutorial series: Hex Map; this file has been updated it to better fit this repository
 *
 *       File Line Length: ~140
 */

using System.Collections.Generic;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// A static reusable pool containing a stack of lists of generic type
    /// </summary>
    public static class ListPool
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static List<T> Get<T>()
        {
            if (ListPoolStack<T>.stack.Count == 0) 
            {
                return new List<T>();
            }
            else 
            {
                return ListPoolStack<T>.stack.Pop();
            }
        }

        // public static List<T> Get<T>(int numberOfDefaultElements)
        // {
        //  return Get<T>(new T[numberOfDefaultElements]);
        // }

        public static List<T> Get<T>(IEnumerable<T> collection)
        {
            if (ListPoolStack<T>.stack.Count == 0) 
            {
                return new List<T>(collection);
            }
            else 
            {
                List<T> list = ListPoolStack<T>.stack.Pop();
                foreach (T element in collection)
                {
                    list.Add(element);
                }
                return list;
            }
        }

        public static void Add<T>(List<T> list)
        {
            Release(ref list);
        }

        public static void Release<T>(ref List<T> list)
        {
            if (list == null) return;

            list.Clear();
            ListPoolStack<T>.stack.Push(list);
            list = null;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Subclasses

        static class ListPoolStack<T>
        {
            public static Stack<List<T>> stack = new Stack<List<T>>();
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}