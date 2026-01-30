/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: December 4, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

using Kokowolo.Utilities;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    /*██████████████████████████████████████████████████████████*/
    #region Functions

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var attr = (ShowIfAttribute)attribute;
        bool enabled = Evaluate(property, attr);

        if (!enabled && attr.HideCompletely)
            return 0f;

        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (ShowIfAttribute)attribute;
        bool enabled = Evaluate(property, attr);

        if (!enabled && attr.HideCompletely)
            return;

        bool prev = GUI.enabled;
        GUI.enabled = enabled;

        EditorGUI.PropertyField(position, property, label, true);

        GUI.enabled = prev;
    }

    private bool Evaluate(SerializedProperty property, ShowIfAttribute attr)
    {
        object owner = GetOwnerObject(property);

        if (owner == null)
            return true;

        object evaluatedOn = owner;

        // Walk up object hierarchy until condition found or root reached
        while (evaluatedOn != null)
        {
            if (TryEvaluateConditionOnObject(evaluatedOn, attr.ConditionName, out bool result))
                return !(result && attr.Negate);

            evaluatedOn = null;
        }

        Debug.LogWarning($"ShowIf: Could not find condition '{attr.ConditionName}' on {owner.GetType()} or its parents.");
        return true;
    }

    private bool TryEvaluateConditionOnObject(object obj, string condition, out bool value)
    {
        value = true;
        Type type = obj.GetType();

        // Try field
        var field = type.GetField(condition, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (field != null && field.FieldType == typeof(bool))
        {
            value = (bool)field.GetValue(obj);
            return true;
        }

        // Try property
        var prop = type.GetProperty(condition, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (prop != null && prop.PropertyType == typeof(bool))
        {
            value = (bool)prop.GetValue(obj);
            return true;
        }

        // Try method
        var method = type.GetMethod(condition, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (method != null && method.ReturnType == typeof(bool) && method.GetParameters().Length == 0)
        {
            value = (bool)method.Invoke(obj, null);
            return true;
        }

        return false;
    }

    private object GetOwnerObject(SerializedProperty property)
    {
        string path = property.propertyPath.Replace(".Array.data[", "[");
        object root = property.serializedObject.targetObject;

        return GetValueFromPath(root, path);
    }

    private object GetValueFromPath(object obj, string path)
    {
        string[] split = path.Split('.');
        object current = obj;
        Type type = obj.GetType();

        for (int i = 0; i < split.Length - 1; i++) // go up to parent, not including final field
        {
            string element = split[i];

            if (element.Contains("["))
            {
                // array element
                int index = int.Parse(element.Substring(element.IndexOf("[") + 1).TrimEnd(']'));
                string arrayName = element.Substring(0, element.IndexOf("["));
                
                var arrField = ReflectionExtensions.GetField(type, arrayName);
                var listObj = arrField?.GetValue(current);

                if (listObj is System.Collections.IList list)
                    current = list[index];
                else if (arrField != null && arrField.FieldType.IsArray)
                    current = ((Array)listObj).GetValue(index);

                type = current.GetType();
            }
            else
            {
                // normal field
                var field = type.GetField(element, ReflectionExtensions.AllFlags);
                if (field == null) return null;

                current = field.GetValue(current);
                if (current == null) return null;

                type = current.GetType();
            }
        }

        return current;
    }

    #endregion
    /*██████████████████████████████████████████████████████████*/
}
