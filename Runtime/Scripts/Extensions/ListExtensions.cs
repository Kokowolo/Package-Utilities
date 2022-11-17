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
    
    #endregion
    /************************************************************/
}