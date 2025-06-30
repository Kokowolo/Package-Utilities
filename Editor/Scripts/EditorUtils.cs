/*
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
using Kokowolo.Utilities;

namespace Kokowolo.Utilities.Editor
{
    public static class EditorUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Texture2D RenderStaticPreview(Sprite sprite, int width, int height)
        {
            if (sprite == null) return null;

            Type type = GetType("UnityEditor.SpriteUtility");
            if (type == null) return null;
            
            MethodInfo method = type.GetMethod("RenderStaticPreview", new[] { typeof(Sprite), typeof(Color), typeof(int), typeof(int) });
            if (method == null) return null;
            
            Texture2D texture = method.Invoke("RenderStaticPreview", new object[] { sprite, Color.white, width, height }) as Texture2D;
            return texture;
        }

        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;

            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {
                var assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    type = assembly.GetType(typeName);
                    if (type != null) return type;
                }
            }
            return null;
        }

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
                EditorUtility.SetDirty(property.serializedObject.targetObject);
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
                EditorUtility.SetDirty(serializedObject.targetObject);
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
        /*██████████████████████████████████████████████████████████*/
    }
}