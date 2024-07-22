/*
 * File Name: ColorEnum.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 19, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorEnum
{
    red,
    green,
    blue,
    white,
    black,
    yellow,
    cyan,
    magenta,
    gray,
    grey,
    clear
}

public static class ColorEnumExtensions
{
    public static Color Color(this ColorEnum colorEnum)
    {
        return colorEnum switch
        {
            ColorEnum.red => UnityEngine.Color.red,
            ColorEnum.green => UnityEngine.Color.green,
            ColorEnum.blue => UnityEngine.Color.blue,
            ColorEnum.white => UnityEngine.Color.white,
            ColorEnum.black => UnityEngine.Color.black,
            ColorEnum.yellow => UnityEngine.Color.yellow,
            ColorEnum.cyan => UnityEngine.Color.cyan,
            ColorEnum.magenta => UnityEngine.Color.magenta,
            ColorEnum.gray => UnityEngine.Color.gray,
            ColorEnum.grey => UnityEngine.Color.grey,
            ColorEnum.clear => UnityEngine.Color.clear,
            _ => throw new System.NotImplementedException()
        };
    }
}