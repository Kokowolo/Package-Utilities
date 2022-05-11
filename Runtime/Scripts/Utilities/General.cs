/**
 * File Name: GenUtils.cs
 * Description: Script that contains general utility functions
 * 
 * Authors: Will Lacey
 * Date Created: October 14, 2020
 * 
 * Additional Comments: 
 *      File Line Length: 120
 *
 *      This script relates to MathUtils.cs
 **/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using Diagnostics = System.Diagnostics;

namespace Kokowolo.Utilities
{
    public static class General
    {
        public static T CacheGetComponent<T>(this MonoBehaviour monoBehaviour, T component) where T : Component
        {
            if (!component) component = monoBehaviour.GetComponent<T>();
            return component;
        }

        #region Mouse Raytracing Functions

        public static bool IsMouseOverGUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        public static Ray MouseScreenPointToRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
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

        #endregion
    }
}