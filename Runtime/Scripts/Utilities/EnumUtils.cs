/*
 * File Name: EnumUtils.cs
 * Description: This script is for basic utility functionality regarding Enums
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 27, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 *
 *      Relates to EnumUtils.cs
 */

using System;
using System.Collections.Generic;

namespace Kokowolo.Utilities
{
    public static class EnumUtils
    {
        /************************************************************/
        #region Functions

        public static T[] GetValues<T>() 
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static T GetLastValue<T>() 
        {   
            T[] values = GetValues<T>();
            return values[values.Length - 1];
        }

        public static int GetCount<T>() 
        {   
            return GetValues<T>().Length;
        }

        #endregion
        /************************************************************/
    }
}