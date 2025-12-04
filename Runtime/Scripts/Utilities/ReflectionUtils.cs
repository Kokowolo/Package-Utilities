/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 27, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

namespace Kokowolo.Utilities
{
    public static class ReflectionUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public const BindingFlags AllFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static FieldInfo GetField<TTarget>(string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags) => GetField(typeof(TTarget), name, searchBaseTypes, flags);
        public static FieldInfo GetField(this Type type, string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags)
        {
            FieldInfo fieldInfo;
            while ((fieldInfo = type.GetField(name, flags)) == null && searchBaseTypes && (type = type.BaseType) != null);
            return fieldInfo;
        }

        public static TMember GetFieldValue<TTarget, TMember>(TTarget target, string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags)
        {
            return (TMember) GetField<TTarget>(name, searchBaseTypes, flags).GetValue(target);
        }

        // public static void SetField<TTarget>(FieldInfo fieldInfo, TTarget target, object value)
        // {
        //     fieldInfo.SetValue(target, value);
        // }

        public static PropertyInfo GetProperty<TTarget>(string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags) => GetProperty(typeof(TTarget), name, searchBaseTypes, flags);
        public static PropertyInfo GetProperty(this Type type, string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags)
        {
           PropertyInfo propertyInfo;
            while ((propertyInfo = type.GetProperty(name, flags)) == null && searchBaseTypes && (type = type.BaseType) != null);
            return propertyInfo;
        }

        public static TMember GetPropertyValue<TTarget, TMember>(TTarget target, string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags)
        {
            return (TMember) GetProperty<TTarget>(name).GetValue(target);
        }

        public static MethodInfo GetMethod<TTarget>(string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags) => GetMethod(typeof(TTarget), name, searchBaseTypes, flags);
        public static MethodInfo GetMethod(this Type type, string name, bool searchBaseTypes = true, BindingFlags flags = AllFlags)
        {
            MethodInfo methodInfo;
            while ((methodInfo = type.GetMethod(name, flags)) == null && searchBaseTypes && (type = type.BaseType) != null);
            return methodInfo;
        }

        // public static Type[] GetSubclasses<TTarget>()
        // {
        //     return typeof(TTarget).Assembly.GetTypes()
        //         .Where(type => type.IsSubclassOf(typeof(TTarget)))
        //         .ToArray();
        // }

        // public static Type GetSubclass<TTarget, TSubclass>()
        // {
        //     return typeof(TTarget).Assembly.GetTypes()
        //         .Where(type => type.IsSubclassOf(typeof(TTarget)))
        //         .Where(type => type.IsSubclassOf(typeof(TSubclass)))
        //         .First();
        // }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}