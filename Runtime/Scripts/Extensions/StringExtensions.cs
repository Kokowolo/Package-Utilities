/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Class for extension functionality regarding strings
    /// </summary>
    public static class StringExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields
        
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Alphanumeric = "0123456789" + Alphabet;
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions
        
        // public static void MyFunction(this string value)
        // {
            
        // }
        
        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        public static string Convert(int value, int toBase)
        {
            if (value == 0) return "0";
            if (toBase < 2 || toBase > 36) throw new ArgumentOutOfRangeException(nameof(toBase), "Base must be in the range [2,36].");

            int number = Math.Abs(value);

            Span<char> buffer = stackalloc char[32]; // enough for int32
            int index = buffer.Length;

            while (number > 0)
            {
                buffer[--index] = Alphanumeric[number % toBase];
                number /= toBase;
            }

            if (value < 0) buffer[--index] = '-';

            return new string(buffer[index..]);
        }

        public static int Convert(string value, int toBase)
        {
            if (toBase < 2 || toBase > 36) throw new ArgumentOutOfRangeException(nameof(toBase));

            value = value.ToUpperInvariant();
            bool isNegative = value[0] == '-';
            int start = isNegative ? 1 : 0;

            int result = 0;

            for (int i = start; i < value.Length; i++)
            {
                int digit = value[i] switch
                {
                    >= '0' and <= '9' => value[i] - '0',
                    >= 'A' and <= 'Z' => value[i] - 'A' + 10,
                    _ => throw new FormatException("Invalid character")
                };

                if (digit >= toBase)
                {
                    throw new FormatException("Digit out of range");
                }

                result = result * toBase + digit;
            }

            return isNegative ? -result : result;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}