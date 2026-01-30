/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 23, 2023
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}