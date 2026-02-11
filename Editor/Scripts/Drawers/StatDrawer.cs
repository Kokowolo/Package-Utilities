/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.  
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 30, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

using Kokowolo.Utilities;

namespace Kokowolo.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(StatReadOnlyMaxAttribute))]
    public class StatReadOnlyMaxDrawer : PropertyDrawer
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try 
            {
                Stat stat = (Stat) property.boxedValue;
                EditorGUI.PropertyField(position, property, label, true); // Draws class below
            }
            catch
            {
                EditorGUI.LabelField(position, label.text, $"Use [{nameof(StatReadOnlyMaxAttribute)}] with {nameof(Stat)}");
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }

    [CustomPropertyDrawer(typeof(Stat))]
    public class StatDrawer : PropertyDrawer
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var serializedProperties = EditorExtensions.GetSerializedProperties<Stat>(property);

            // NOTE: header, field, and spacing need to add to 1
            float headerWidthFactor     = 0.3f;
            float spacingWidthFactor    = 0.1f;

            float headerWidth       = headerWidthFactor * position.width;
            float spacing           = spacingWidthFactor * position.width / serializedProperties.Count;
            float fieldWidth        = (1f - headerWidthFactor - spacingWidthFactor) * position.width / serializedProperties.Count;
            float prefixLabelWidth  = 0.5f * fieldWidth;

            float x = position.x;
            EditorGUI.LabelField(new Rect(x, position.y, headerWidth, position.height), label);
            x += headerWidth + spacing;
            
            for (int i = 0; i < serializedProperties.Count; i++)
            {
                Rect rect;
                rect = new Rect(x, position.y, fieldWidth, position.height);
                GUIContent prefix = new GUIContent($"{serializedProperties[i].displayName}");
                EditorGUI.PrefixLabel(rect, prefix);
                rect = new Rect(x + prefixLabelWidth, position.y, fieldWidth - prefixLabelWidth, position.height);
                EditorGUI.PropertyField(rect, serializedProperties[i], GUIContent.none);
                x += fieldWidth + (i == serializedProperties.Count - 1 ? 0 : spacing);
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}