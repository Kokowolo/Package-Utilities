/*
 * File Name: EditorUtilities.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 24, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;
using System.Linq;
using UnityEditor;
using Kokowolo.Utilities;

namespace Kokowolo.Utilities.Editor
{
    public static class EditorUtils
    {
        /************************************************************/
        #region Functions

        /// <summary>
        /// Returns custom fields for this RuleTile
        /// </summary>
        /// <param name="isOverrideInstance">Whether override fields are returned</param>
        /// <returns>Custom fields for this RuleTile</returns>
        public static FieldInfo[] GetSerializeFields<T>(T target/*bool isOverrideInstance*/)
        {
            return target.GetType().GetFields(ReflectionUtils.AllFlags)
                .Where(field => field.IsPublic || field.IsDefined(typeof(SerializeField)))
                .Where(field => !field.IsDefined(typeof(HideInInspector)))
                .ToArray();
        }

        public static void DrawSerializeFields<T>(T target, SerializedProperty property)
        {
            FieldInfo[] serializeFields = GetSerializeFields(target);
            property.serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            foreach (FieldInfo field in serializeFields)
            {
                var prop = property.FindPropertyRelative(field.Name);
                if (prop != null)
                {
                    EditorGUILayout.PropertyField(prop, true);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        public static void DrawSerializeFields<T>(T target, SerializedObject serializedObject)
        {
            FieldInfo[] serializeFields = GetSerializeFields(target);
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            foreach (FieldInfo field in serializeFields)
            {
                var prop = serializedObject.FindProperty(field.Name);
                if (prop != null)
                {
                    EditorGUILayout.PropertyField(prop, true);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        // public static bool DrawCustomFields(SerializedProperty property, SerializedObject serializedObject)
        // {
        //     var customFields = GetSerializeFields(property);

        //     serializedObject.Update();
        //     EditorGUI.BeginChangeCheck();
        //     foreach (var field in customFields)
        //     {
        //         var property = serializedObject.FindProperty(field.Name);
        //         if (property != null)
        //         {
        //             EditorGUILayout.PropertyField(property, true);
        //         }
        //     }

        //     if (EditorGUI.EndChangeCheck())
        //     {
        //         serializedObject.ApplyModifiedProperties();
        //         return true;
        //     }
        //     return false;
        // }

        #endregion
        /************************************************************/
    }
}