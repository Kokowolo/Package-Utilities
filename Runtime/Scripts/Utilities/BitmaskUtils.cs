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

        public static bool Contains(int mask, int index)
        {
            // NOTE: 1 is shifted `layer` times; 000000001 -> 000010000 for layer 4
            return (mask & (1 << index)) != 0;
        }
        
        public static bool Contains(uint mask, int index)
        {
            // NOTE: 1 is shifted `layer` times; 000000001 -> 000010000 for layer 4
            return (mask & (1 << index)) != 0;
        }

        public static uint RotateLeft(uint mask, int rotations)
        {
            return RotateLeft(mask, (byte) rotations);
        }

        public static uint RotateLeft(uint mask, byte rotations)
        {
            return (mask << rotations) | (mask >> (32 - rotations));
        }

        public static uint RotateLeft(uint mask, int rotations, int numberOfBits)
        {
            return RotateLeft(mask, (byte) rotations, numberOfBits);
        }

        public static uint RotateLeft(uint mask, byte rotations, int numberOfBits)
        {
            uint shift = RotateLeft(mask, rotations);
            return mask == shift ? mask : shift % ((uint) (Mathf.Pow(2, numberOfBits) - 1));
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}