/**
 * File Name: General.cs
 * Description: Script that contains general utility functions
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 14, 2020
 * 
 * Additional Comments: 
 *      File Line Length: 120
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class General
    {
        public static bool IsGameObjectInLayerMask(GameObject gameObject, LayerMask layerMask)
        {
            return (1 << gameObject.layer & layerMask) != 0;
        }

        public static T CacheGetComponent<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
        {
            if (!component) component = monoBehaviour.GetComponent<T>();
            return component;
        }

        /// <summary>
        /// Recursively searches children/grandchildren to find a child by name n and return it
        /// </summary>
        /// <returns>The found child transform; Null if child with matching name isn't found</returns>
        public static Transform RecursiveFind(this Transform transform, string n)
        {
            foreach (Transform child in transform)
            {
                if (child.name == n) return child;
                Transform grandchild = child.RecursiveFind(n);
                if (grandchild) return grandchild;
            }
            return null;
        }

        public static string GetPersistentDataPath()
        {
            // https://github.com/Kokowolo/Project-Fort/blob/Version-2.1-Alpha/Assets/Scripts/UI/SaveLoadMenu.cs
            // Application.dataPath;
            // System.IO.Directory.GetCurrentDirectory()
            // Path.Combine(Application.persistentDataPath, mapName + ".map.bytes");

            // string path = Application.dataPath;
            // path = Path.Combine(path, "Assets");
            
            // Debug.Log(path);
            // DirectoryInfo info = new DirectoryInfo(path);
            // FileInfo[] fileInfo = info.GetFiles();
            // foreach (var file in fileInfo) Debug.Log(file);
            
            return Application.persistentDataPath;
        }
    }
}