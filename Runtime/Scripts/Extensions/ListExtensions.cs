/*
 * File Name: StringExtensions.cs
 * Description: This script is for extension functionality regarding strings
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

    public static string ToStringForElements<T>(this List<T> list)
    {
        string str = "[ ";
        for (int i = 0; i < list.Count - 1; i++)
        {
            str += $"{list[i].ToString()}, ";
        }
        if (list.Count != 0) str += $"{list[list.Count - 1].ToString()} ]";
        else str += "]";
        return str;
    }
    
    #endregion
    /************************************************************/
}