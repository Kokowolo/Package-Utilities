﻿/**
 * File Name: ListPool.cs
 * Description: This script is for a static reusable pool containing a stack of lists of generic type
 * 
 * Authors: Catlike Coding, Will Lacey
 * Date Created: September 24, 2020
 * 
 * Additional Comments: 
 *      The original version of this file can be found here: https://catlikecoding.com/unity/tutorials/hex-map/ within 
 *		Catlike Coding's tutorial series: Hex Map; this file has been updated it to better fit this repository
 *
 *		File Line Length: 120
 **/

using System.Collections.Generic;

namespace Kokowolo.Utilities
{
	public static class ListPool
	{
		/************************************************************/
		#region Fields

		// private static Stack<List<T>> stack = new Stack<List<T>>();

		#endregion
		/************************************************************/
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

        public static List<T> Get<T>(IEnumerable<T> collection)
		{
            List<T> list = Get<T>();
            foreach (T element in collection)
            {
                list.Add(element);
            }
            return list;
		}

		public static void Release<T>(List<T> list)
		{
			list.Clear();
			ListPoolStack<T>.stack.Push(list);
		}

		#endregion
		/************************************************************/
        #region Subclasses

        private static class ListPoolStack<T>
        {
            public static Stack<List<T>> stack = new Stack<List<T>>();
        }

        #endregion
        /************************************************************/
	}
}