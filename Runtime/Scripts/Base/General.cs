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
        [System.Obsolete("please write an encapsulated property delcaration instead")]
        public static T CacheGetComponent<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
        {
            if (!component) component = monoBehaviour.GetComponent<T>();
            return component;
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