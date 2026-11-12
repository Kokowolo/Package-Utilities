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
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatDrawer : PropertyDrawer
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields
        
        // NOTE: header, field, and spacing need to add to 1
        private const float HeaderWidthFactor       = 0.3f;
        private const float SpacingWidthFactor      = 0.1f;
        private const float PrefixLabelWidthFactor  = 0.5f;

        private static readonly GUIContent tempContent = new GUIContent();

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Enables prefab override bolding, right-click revert/copy/paste, and multi-edit mixed values for the whole row
            label = EditorGUI.BeginProperty(position, label, property);

            var serializedProperties = EditorExtensions.GetSerializedProperties<Stat>(property);
            int count = serializedProperties.Count;

            float headerWidth = HeaderWidthFactor * position.width;
            EditorGUI.LabelField(new Rect(position.x, position.y, headerWidth, position.height), label); // respects indent

            if (count == 0)
            {
                EditorGUI.EndProperty();
                return;
            }

            float spacing           = SpacingWidthFactor * position.width / count;
            float fieldWidth        = (1f - HeaderWidthFactor - SpacingWidthFactor) * position.width / count;
            float prefixLabelWidth  = PrefixLabelWidthFactor * fieldWidth;

            // Child rects are already laid out manually, so indent would only shift/squish them when nested
            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            try
            {
                float x = position.x + headerWidth + spacing;
                for (int i = 0; i < count; i++)
                {
                    SerializedProperty child = serializedProperties[i];

                    // LabelField clips to its rect, unlike PrefixLabel which sizes itself from EditorGUIUtility.labelWidth
                    tempContent.text    = child.displayName;
                    tempContent.tooltip = child.tooltip;
                    EditorGUI.LabelField(new Rect(x, position.y, prefixLabelWidth, position.height), tempContent);

                    Rect fieldRect = new Rect(x + prefixLabelWidth, position.y, fieldWidth - prefixLabelWidth, position.height);
                    EditorGUI.PropertyField(fieldRect, child, GUIContent.none);

                    x += fieldWidth + spacing;
                }
            }
            finally
            {
                EditorGUI.indentLevel = indentLevel;
                EditorGUI.EndProperty();
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}