/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 17, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities
{
    [Flags]
    public enum Axis
    {
        None = 0,
        XPos = 1,
        XNeg = 2,
        YPos = 4,
        YNeg = 8,
        ZPos = 16,
        ZNeg = 32,
        
        X = 3,
        Y = 12,
        Z = 48,
    }

    public static class AxisExtensions
    {
        public static Axis Positive(this Axis axis)
        {
            return axis switch
            {
                Axis.X => Axis.XPos,
                Axis.XNeg => Axis.XPos,
                Axis.Y => Axis.YPos,
                Axis.YNeg => Axis.YPos,
                Axis.Z => Axis.ZPos,
                Axis.ZNeg => Axis.ZPos,
                _ => axis
            };
        }

        public static Axis Negative(this Axis axis)
        {
            return axis switch
            {
                Axis.X => Axis.XNeg,
                Axis.XPos => Axis.XNeg,
                Axis.Y => Axis.YNeg,
                Axis.YPos => Axis.YNeg,
                Axis.Z => Axis.ZNeg,
                Axis.ZPos => Axis.ZNeg,
                _ => axis
            };
        }

        public static Axis Opposite(this Axis axis)
        {
            return axis switch
            {
                Axis.XPos => Axis.XNeg,
                Axis.XNeg => Axis.XPos,
                Axis.YPos => Axis.YNeg,
                Axis.YNeg => Axis.YPos,
                Axis.ZPos => Axis.ZNeg,
                Axis.ZNeg => Axis.ZPos,
                _ => axis
            };
        }

        public static Vector3 ToVector3(this Axis axis)
        {
            return axis switch
            {
                Axis.XPos => Vector3.right,
                Axis.XNeg => Vector3.left,
                Axis.YPos => Vector3.up,
                Axis.YNeg => Vector3.down,
                Axis.ZPos => Vector3.forward,
                Axis.ZNeg => Vector3.back,
                _ => Vector3.zero
            };
        }

        public static Vector3Int ToVector3Int(this Axis axis)
        {
            return axis switch
            {
                Axis.XPos => Vector3Int.right,
                Axis.XNeg => Vector3Int.left,
                Axis.YPos => Vector3Int.up,
                Axis.YNeg => Vector3Int.down,
                Axis.ZPos => Vector3Int.forward,
                Axis.ZNeg => Vector3Int.back,
                _ => Vector3Int.zero
            };
        }
    }
}