/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: December 4, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ShowIfAttribute : PropertyAttribute
{
    /*██████████████████████████████████████████████████████████*/
    #region Properties

    public string ConditionName { get; }
    public bool HideCompletely { get; }
    public bool Negate { get; }

    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Functions

    public ShowIfAttribute(string conditionName, bool hideCompletely = false, bool negate = false)
    {
        ConditionName = conditionName;
        HideCompletely = hideCompletely;
        Negate = negate;
    }

    #endregion
    /*██████████████████████████████████████████████████████████*/
}
