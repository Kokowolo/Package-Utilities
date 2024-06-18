/*
 * File Name: Axis.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 17, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
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
                Axis.Y => Axis.YPos,
                Axis.Z => Axis.ZPos,
                _ => throw new NotImplementedException()
            };
        }

        public static Axis Negative(this Axis axis)
        {
            return axis switch
            {
                Axis.X => Axis.XNeg,
                Axis.Y => Axis.YNeg,
                Axis.Z => Axis.ZNeg,
                _ => throw new NotImplementedException()
            };
        }
    }
}