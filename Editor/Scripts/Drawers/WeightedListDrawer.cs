/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 11, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kokowolo.Utilities
{
    [CustomPropertyDrawer(typeof(WeightedList<>), true)]
    public class WeightedListDrawer : PropertyDrawer
    {
        float LineGap => 2f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var itemsProp = property.FindPropertyRelative("items");
            float height = EditorGUIUtility.singleLineHeight + LineGap; // foldout line

            if (itemsProp.isExpanded)
            {
                for (int i = 0; i < itemsProp.arraySize; i++)
                {
                    var itemProp = itemsProp.GetArrayElementAtIndex(i);
                    var valueField = itemProp.FindPropertyRelative("value");

                    float valueHeight = EditorGUI.GetPropertyHeight(valueField, true);
                    float blockHeight = EditorGUIUtility.singleLineHeight + LineGap + valueHeight + LineGap;

                    height += blockHeight;
                }

                height += EditorGUIUtility.singleLineHeight + LineGap * 2; // Add button
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var itemsProp = property.FindPropertyRelative("items");
            EditorGUI.BeginProperty(position, label, property);

            // Foldout
            itemsProp.isExpanded = EditorGUI.Foldout(
                new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                itemsProp.isExpanded,
                label,
                true
            );

            if (itemsProp.isExpanded)
            {
                EditorGUI.indentLevel++;
                float y = position.y + EditorGUIUtility.singleLineHeight + LineGap;

                for (int i = 0; i < itemsProp.arraySize; i++)
                {
                    var itemProp = itemsProp.GetArrayElementAtIndex(i);
                    var valueField = itemProp.FindPropertyRelative("value");
                    var weightField = itemProp.FindPropertyRelative("weight");
                    var percentField = itemProp.FindPropertyRelative("percent");

                    float valueHeight = EditorGUI.GetPropertyHeight(valueField, true);
                    float blockStartY = y;

                    // Header row rects
                    float fullWidth = position.width;
                    float weightWidth = fullWidth * 0.25f;
                    float percentWidth = fullWidth * 0.20f;
                    float removeWidth = fullWidth * 0.10f;
                    float labelWidth = fullWidth * 0.40f;

                    var labelRect = new Rect(position.x, blockStartY, labelWidth, EditorGUIUtility.singleLineHeight);
                    var weightRect = new Rect(position.x + labelWidth + 5f, blockStartY, weightWidth, EditorGUIUtility.singleLineHeight);
                    var percentRect = new Rect(position.x + labelWidth + weightWidth + 10f, blockStartY, percentWidth, EditorGUIUtility.singleLineHeight);
                    var removeRect = new Rect(position.x + labelWidth + weightWidth + percentWidth + 15f, blockStartY, removeWidth, EditorGUIUtility.singleLineHeight);

                    // Header labels and fields
                    GUIStyle labelStyle = new GUIStyle();
                    labelStyle.normal.textColor = Color.white;
                    // labelStyle.fontStyle = FontStyle.Bold;
                    labelStyle.richText = true;
                    EditorGUI.LabelField(labelRect, $"<u>Item {i}</u>", labelStyle);
                    EditorGUI.PropertyField(weightRect, weightField, GUIContent.none);
                    EditorGUI.LabelField(percentRect, $"{percentField.floatValue:F2}%");

                    if (GUI.Button(removeRect, "—"))
                    {
                        itemsProp.DeleteArrayElementAtIndex(i);
                        break;
                    }

                    // Item content below header
                    var itemRect = new Rect(position.x, blockStartY + EditorGUIUtility.singleLineHeight + LineGap, fullWidth, valueHeight);
                    EditorGUI.PropertyField(itemRect, valueField, GUIContent.none, true);

                    y += EditorGUIUtility.singleLineHeight + valueHeight + LineGap * 2;
                }

                if (GUI.Button(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), "Add Weighted Item"))
                {
                    itemsProp.InsertArrayElementAtIndex(itemsProp.arraySize);
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }
    }
}