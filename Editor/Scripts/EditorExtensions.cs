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

        public static FieldInfo[] GetSerializeFields<T>(T target/*bool isOverrideInstance*/)
        {
            return target.GetType().GetFields(ReflectionExtensions.AllFlags)
                .Where(field => field.IsPublic || field.IsDefined(typeof(SerializeField)))
                .Where(field => !field.IsDefined(typeof(HideInInspector)))
                .ToArray();
        }

        public static void DrawSerializeFields<T>(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                DrawSerializeFields<T>(property);
                EditorGUI.indentLevel--;
            }
        }

        public static void DrawSerializeFields<T>(SerializedProperty property) => DrawSerializeFields((T) property.boxedValue, property);
        public static void DrawSerializeFields<T>(T target, SerializedProperty property)
        {
            FieldInfo[] serializeFields = GetSerializeFields(target);
            // property.serializedObject.Update();
            property.serializedObject.UpdateIfRequiredOrScript();
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
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }

        public static void DrawSerializeFields(UnityEditor.Editor editor) => DrawSerializeFields(editor.target, editor.serializedObject);
        public static void DrawSerializeFields<T>(T target, SerializedObject serializedObject)
        {
            FieldInfo[] serializeFields = GetSerializeFields(target);
            // serializedObject.Update();
            serializedObject.UpdateIfRequiredOrScript();
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
                EditorUtility.SetDirty(serializedObject.targetObject);
            }
        }

        /* // from UnityEditor.Editor.cs
        public bool DoDrawDefaultInspector()
        {
            bool result;
            using (new LocalizationGroup(target))
            {
                result = DoDrawDefaultInspector(serializedObject);
                MonoBehaviour monoBehaviour = target as MonoBehaviour;
                if (monoBehaviour == null )//|| !AudioUtil.HasAudioCallback(monoBehaviour) || AudioUtil.GetCustomFilterChannelCount(monoBehaviour) <= 0)
                {
                    return result;
                }

                // if (m_AudioFilterGUI == null)
                // {
                //     m_AudioFilterGUI = new AudioFilterGUI();
                // }

                // m_AudioFilterGUI.DrawAudioFilterGUI(monoBehaviour);
            }

            return result;
        }

        public static bool DoDrawDefaultInspector(SerializedObject obj)
        {
            EditorGUI.BeginChangeCheck();
            obj.UpdateIfRequiredOrScript();
            SerializedProperty iterator = obj.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }

                enterChildren = false;
            }

            obj.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }
        // */

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}