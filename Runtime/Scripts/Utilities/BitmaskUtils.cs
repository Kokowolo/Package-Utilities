/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 27, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class BitmaskUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static uint RotateLeft(uint value, int rotations)
        {
            return RotateLeft(value, (byte) rotations);
        }

        public static uint RotateLeft(uint value, byte rotations)
        {
            return (value << rotations) | (value >> (32 - rotations));
        }

        public static uint RotateLeft(uint value, int rotations, int numberOfBits)
        {
            return RotateLeft(value, (byte) rotations, numberOfBits);
        }

        public static uint RotateLeft(uint value, byte rotations, int numberOfBits)
        {
            uint shift = RotateLeft(value, rotations);
            return value == shift ? value : shift % ((uint) (Mathf.Pow(2, numberOfBits) - 1));
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}