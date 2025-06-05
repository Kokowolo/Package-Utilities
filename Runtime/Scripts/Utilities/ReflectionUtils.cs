/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 27, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

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

        public static BindingFlags AllFlags => BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static FieldInfo GetField<TTarget>(string name)
        {
            return typeof(TTarget).GetField(name, AllFlags);
        }

        public static TMember GetFieldValue<TTarget, TMember>(TTarget target, string name)
        {
            return (TMember) GetField<TTarget>(name).GetValue(target);
        }

        public static PropertyInfo GetProperty<TTarget>(string name)
        {
            return typeof(TTarget).GetProperty(name, AllFlags);
        }

        public static TMember GetPropertyValue<TTarget, TMember>(TTarget target, string name)
        {
            return (TMember) GetProperty<TTarget>(name).GetValue(target);
        }

        // public static void SetField<TTarget>(FieldInfo fieldInfo, TTarget target, object value)
        // {
        //     fieldInfo.SetValue(target, value);
        // }

        public static MethodInfo GetMethod<TTarget>(string name)
        {
            return typeof(TTarget).GetMethod(name, AllFlags);
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