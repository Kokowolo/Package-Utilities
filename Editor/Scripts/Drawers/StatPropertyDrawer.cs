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
    public class StatReadOnlyMaxAttributePropertyDrawer : PropertyDrawer
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
    public class StatPropertyDrawer : PropertyDrawer
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            // Initalize variables and get serializeFields
            StatReadOnlyMaxAttribute attribute = fieldInfo.GetCustomAttribute<StatReadOnlyMaxAttribute>();
            Stat stat = (Stat) property.boxedValue;
            bool modifiedProperties = false;
            float headerWidth = rect.width * 0.3f;
            float fieldLabelWidth = rect.width * 0.15f;
            float fieldValueWidth = rect.width * 0.2f;
            float positionX = rect.x;
            
            // Get serializedFields
            var serializeFields = EditorExtensions.GetSerializeFields(stat);
            var maxValueProperty = property.FindPropertyRelative(serializeFields[0].Name);
            var currentValueProperty = property.FindPropertyRelative(serializeFields[1].Name);

            // Draw Label
            EditorGUI.LabelField(new Rect(positionX, rect.y, headerWidth, rect.height), label);
            positionX += headerWidth;

            // Draw maxValue property
            {
                bool enabled = GUI.enabled;
                GUI.enabled = enabled && (attribute == null || !attribute.ReadOnlyMax);
                EditorGUI.BeginChangeCheck();
                EditorGUI.LabelField(new Rect(positionX, rect.y, fieldLabelWidth, rect.height), new GUIContent("Max"));
                positionX += fieldLabelWidth;
                EditorGUI.PropertyField(
                    new Rect(positionX, rect.y, fieldValueWidth, rect.height), 
                    maxValueProperty, 
                    GUIContent.none, 
                    true
                );
                positionX += fieldValueWidth;
                if (EditorGUI.EndChangeCheck())
                {
                    maxValueProperty.floatValue = Mathf.Max(0, maxValueProperty.floatValue);
                    currentValueProperty.floatValue = Mathf.Clamp(currentValueProperty.floatValue, 0, maxValueProperty.floatValue);
                    modifiedProperties = true;
                }
                GUI.enabled = enabled;
            }

            // Draw currentValue property
            {
                EditorGUI.BeginChangeCheck();
                EditorGUI.LabelField(new Rect(positionX, rect.y, fieldLabelWidth, rect.height), new GUIContent("Current"));
                positionX += fieldLabelWidth;
                EditorGUI.PropertyField(
                    new Rect(positionX, rect.y, fieldValueWidth, rect.height), 
                    currentValueProperty, 
                    GUIContent.none, 
                    true
                );
                if (EditorGUI.EndChangeCheck())
                {
                    currentValueProperty.floatValue = Mathf.Clamp(currentValueProperty.floatValue, 0, maxValueProperty.floatValue);
                    modifiedProperties = true;
                }
            }

            // Finish drawer and save fields
            if (modifiedProperties)
            {
                property.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }

        // NOTE: this does the exact same thing as above, but uses a Foldout instead rather than drawing fields in one line
        // public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        // {
        //     // Start drawer foldout
        //     property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
        //     if (!property.isExpanded) return;
        //     EditorGUI.indentLevel++;

        //     // Initalize variables and get serializeFields
        //     StatReadOnlyMaxAttribute attribute = fieldInfo.GetCustomAttribute<StatReadOnlyMaxAttribute>();
        //     Stat stat = (Stat) property.boxedValue;
        //     bool modifiedProperties = false;
            
        //     // Get serializedFields
        //     var serializeFields = EditorUtils.GetSerializeFields(stat);
        //     var maxValueProperty = property.FindPropertyRelative(serializeFields[0].Name);
        //     var currentValueProperty = property.FindPropertyRelative(serializeFields[1].Name);

        //     // Draw maxValue property
        //     {
        //         GUI.enabled = attribute == null || !attribute.ReadOnlyMax;
        //         EditorGUI.BeginChangeCheck();
        //         EditorGUILayout.PropertyField(maxValueProperty, true);
        //         if (EditorGUI.EndChangeCheck())
        //         {
        //             maxValueProperty.floatValue = Mathf.Max(0, maxValueProperty.floatValue);
        //             currentValueProperty.floatValue = Mathf.Clamp(currentValueProperty.floatValue, 0, maxValueProperty.floatValue);
        //             modifiedProperties = true;
        //         }
        //         GUI.enabled = true;
        //     }

        //     // Draw currentValue property
        //     {
        //         EditorGUI.BeginChangeCheck();
        //         EditorGUILayout.PropertyField(currentValueProperty, true);
        //         if (EditorGUI.EndChangeCheck())
        //         {
        //             currentValueProperty.floatValue = Mathf.Clamp(currentValueProperty.floatValue, 0, maxValueProperty.floatValue);
        //             modifiedProperties = true;
        //         }
        //     }

        //     // Close drawer and save fields
        //     EditorGUI.indentLevel--;
        //     if (modifiedProperties)
        //     {
        //         property.serializedObject.ApplyModifiedProperties();
        //         EditorUtility.SetDirty(property.serializedObject.targetObject);
        //     }
        // }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}