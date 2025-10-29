/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 17, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

#if UNITY_EDITOR

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    public static class EditorOnlyUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static T FindFirstAssetByType<T>() where T : UnityEngine.Object => (T) FindFirstAssetByType(typeof(T));
        public static object FindFirstAssetByType(Type type)
        {
            if (!type.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                LogManager.LogError($"type {type} is not a UnityEngine.Object");
                return null;
            }
            var guid = AssetDatabase.FindAssets($"t:{type.Name}")[0];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, type);
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

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}

#endif