/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 27, 2022
 * 
 * Additional Comments:
 *      File Line Length: ~140
 *
 *      Relates to EnumExtensions.cs
 */

using System;
using System.Collections.Generic;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Basic utility functionality regarding Enums
    /// </summary>
    public static class EnumUtils
    {
        /*██████████████████████████████████████████████████████████*/
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
        /*██████████████████████████████████████████████████████████*/
    }
}