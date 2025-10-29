/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 19, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class HeaderLineAttribute : PropertyAttribute
{
    /*██████████████████████████████████████████████████████████*/
    #region Fields

    public const float DefaultHeight = 2.0f;

    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Properties

    public float Height { get; private set; }
    public Color Color { get; private set; }

    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Functions

    private HeaderLineAttribute(float height, Color color)
    {
        Height = height;
        Color = color;
    }

    public HeaderLineAttribute() : this(DefaultHeight, Color.grey) {}

    public HeaderLineAttribute(float height) : this(height, Color.grey) {}

    public HeaderLineAttribute(ColorEnum colorEnum) : this(DefaultHeight, colorEnum.Color()) {}

    public HeaderLineAttribute(float height, ColorEnum colorEnum) : this(height, colorEnum.Color()) {}

    public HeaderLineAttribute(float r, float g, float b) : this(DefaultHeight, new Color(r, b, g)) {}
    
    public HeaderLineAttribute(float height, float r, float g, float b) : this(height, new Color(r, b, g)) {}

    #endregion
    /*██████████████████████████████████████████████████████████*/
}