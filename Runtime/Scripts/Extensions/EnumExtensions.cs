/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *      File Line Length: ~140
 *
 *      Relates to EnumUtils.cs
 */

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Class is for extension functionality regarding Enums
    /// </summary>
    public static class EnumExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static string ToStringFromPascalCase(this Enum value)
        {
            string str = "";
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            for (int i = 1; i <= name.Length; i++)
            {
                str += name[i - 1];
                if (i < name.Length && char.IsUpper(name[i])) str += " ";   
            }
            return str;
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute a) return a.Description;
                }
            }
            return value.ToString();
        }

        /// <summary>Returns the next defined value of the enum. Wraps to the first value by default.</summary>
        public static T Next<T>(this T value, bool wrap = true) where T : struct, Enum
        {
            T[] values = GetValues<T>();
            int index = Array.IndexOf(values, value);
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(value), value, $"{value} is not a defined {typeof(T).Name} value.");

            int next = index + 1;
            if (next < values.Length) return values[next];
            return wrap ? values[0] : values[^1];
        }

        /// <summary>Returns the previous defined value of the enum. Wraps to the last value by default.</summary>
        public static T Previous<T>(this T value, bool wrap = true) where T : struct, Enum
        {
            T[] values = GetValues<T>();
            int index = Array.IndexOf(values, value);
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(value), value, $"{value} is not a defined {typeof(T).Name} value.");

            int prev = index - 1;
            if (prev >= 0) return values[prev];
            return wrap ? values[^1] : values[0];
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        public static T[] GetValues<T>() where T : struct, Enum
        {
            // NOTE: this does not cache or remove instances where enums are equal, i.e. { Dog = 0, Cat = 0}
            // return (T[])Enum.GetValues(typeof(T));
            return EnumCache<T>.Values;
        }

        public static T GetLastValue<T>() where T : struct, Enum
        {   
            return GetValues<T>()[^1];
        }

        public static int GetCount<T>() where T : struct, Enum
        {   
            return GetValues<T>().Length;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Subclasses
        
        private static class EnumCache<T> where T : struct, Enum
        {
            static T[] _Values;
            public static T[] Values
            {
                get
                {
                    if (_Values == null)
                    {
                        _Values = Enum.GetValues(typeof(T)).Cast<T>().Distinct().ToArray();
                        LogManager.LogWarning($"Caching values for Enum {nameof(T)}");
                    }
                    return _Values;
                }
            }
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}