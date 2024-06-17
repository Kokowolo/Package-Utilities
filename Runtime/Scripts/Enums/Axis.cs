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
}