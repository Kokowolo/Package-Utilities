/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 24, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;
using System.Linq;
using UnityEditor;
// using Kokowolo.Utilities;

namespace Kokowolo.Utilities.Editor
{
    public static class EditorExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Texture2D RenderStaticPreview(Sprite sprite, int width, int height)
        {
            if (sprite == null) return null;

            Type type = EditorOnlyUtils.GetType("UnityEditor.SpriteUtility");
            if (type == null) return null;
            
            MethodInfo method = type.GetMethod("RenderStaticPreview", new[] { typeof(Sprite), typeof(Color), typeof(int), typeof(int) });
            if (method == null) return null;
            
            Texture2D texture = method.Invoke("RenderStaticPreview", new object[] { sprite, Color.white, width, height }) as Texture2D;
            return texture;
        }

        public static FieldInfo[] GetSerializeFields<T>() => GetSerializeFields(typeof(T));
        public static FieldInfo[] GetSerializeFields(Type type)
        {
            return type.GetFields(ReflectionExtensions.AllFlags)
                .Where(field => field.IsPublic || field.IsDefined(typeof(SerializeField)))
                .Where(field => !field.IsDefined(typeof(HideInInspector)))
                .ToArray();
        }

        public static List<SerializedProperty> GetSerializedProperties<T>(SerializedProperty property) => GetSerializedProperties(typeof(T), property);
        public static List<SerializedProperty> GetSerializedProperties(Type type, SerializedProperty property)
        {
            List<SerializedProperty> properties = new List<SerializedProperty>();
            foreach (FieldInfo field in GetSerializeFields(type))
            {
                var prop = property.FindPropertyRelative(field.Name);
                if (prop != null) // is this null check even needed?
                {
                    properties.Add(prop);
                }
            }
            return properties;
        }

        public static List<SerializedProperty> GetSerializedProperties<T>(SerializedObject serializedObject) => GetSerializedProperties(typeof(T), serializedObject);
        public static List<SerializedProperty> GetSerializedProperties(Type type, SerializedObject serializedObject)
        {
            List<SerializedProperty> properties = new List<SerializedProperty>();
            foreach (FieldInfo field in GetSerializeFields(type))
            {
                var prop = serializedObject.FindProperty(field.Name);
                if (prop != null) // is this null check even needed?
                {
                    properties.Add(prop);
                }
            }
            return properties;
        }

        public static float GetPropertyHeight<T>(SerializedProperty property) => GetPropertyHeight(typeof(T), property);
        public static float GetPropertyHeight(Type type, SerializedProperty property)
        {
            float height = EditorGUIUtility.singleLineHeight;
            if (property.isExpanded)
            {
                foreach (var prop in GetSerializedProperties(type, property))
                {
                    height += EditorGUI.GetPropertyHeight(prop);
                }
            }
            return height;
        }

        public static void DrawSerializeFields<T>(Rect position, SerializedProperty property, GUIContent label) => DrawSerializeFields(typeof(T), position, property, label);
        public static void DrawSerializeFields(Type type, Rect position, SerializedProperty property, GUIContent label)
        {
            // Start SerializedProperty
            property.serializedObject.UpdateIfRequiredOrScript();
            EditorGUI.BeginChangeCheck();

            // Draw foldout
            position = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
            position.y += position.height;
            if (property.isExpanded)
            {
                // Enter foldout
                EditorGUI.indentLevel++;

                // Draw SerializedProperties
                var properties = GetSerializedProperties(type, property);
                foreach (var prop in properties)
                {
                    position = new Rect(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(prop));
                    EditorGUI.PropertyField(position, prop);
                    position.y += position.height;
                }

                // Exit foldout
                EditorGUI.indentLevel--;
            }
            
            // End SerializedProperty
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }

        // NOTE: the following does auto layout, consider calling EditorGUI variant of this function instead
        public static void DrawSerializeFields<T>(SerializedProperty property) => DrawSerializeFields(typeof(T), property);
        public static void DrawSerializeFields(Type type, SerializedProperty property)
        {
            // Start SerializedProperty
            property.serializedObject.UpdateIfRequiredOrScript();
            EditorGUI.BeginChangeCheck();

            // Draw SerializedProperties
            foreach (SerializedProperty prop in GetSerializedProperties(type, property))
            {
                EditorGUILayout.PropertyField(prop, true);
            }

            // End SerializedProperty
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }

        [Obsolete("use DrawSerializeFields(Type type, SerializedObject property) instead")]
        public static void DrawSerializeFields(UnityEditor.Editor editor) => DrawSerializeFields(editor.target, editor.serializedObject);
        [Obsolete("use DrawSerializeFields(Type type, SerializedObject property) instead")]
        public static void DrawSerializeFields<T>(T target, SerializedObject serializedObject) => DrawSerializeFields(typeof(T), serializedObject);

        public static void DrawSerializeFields<T>(SerializedObject serializedObject) => DrawSerializeFields(typeof(T), serializedObject);
        public static void DrawSerializeFields(Type type, SerializedObject serializedObject)
        {
            // Start SerializedObject
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUI.BeginChangeCheck();

            // Draw SerializedProperties
            foreach (SerializedProperty prop in GetSerializedProperties(type, serializedObject))
            {
                EditorGUILayout.PropertyField(prop, true);
            }

            // End SerializedObject
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(serializedObject.targetObject);
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}