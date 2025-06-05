/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 19, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomPropertyDrawer(typeof(HeaderLineAttribute))]
public class HeaderLineDecoratorDrawer : DecoratorDrawer
{
    /*██████████████████████████████████████████████████████████*/
    #region Properties

    HeaderLineAttribute Attribute => (HeaderLineAttribute) attribute;

    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Functions

    public override void OnGUI(Rect position)
    {
        Rect rect = EditorGUI.IndentedRect(position);
        rect.y += EditorGUIUtility.singleLineHeight / 3.0f;
        
        rect.height = Attribute.Height;
        EditorGUI.DrawRect(rect, Attribute.Color);
    }

    public override float GetHeight()
    {
        return 0.5f * (EditorGUIUtility.singleLineHeight + Attribute.Height);
    }

    #endregion
    /*██████████████████████████████████████████████████████████*/
}