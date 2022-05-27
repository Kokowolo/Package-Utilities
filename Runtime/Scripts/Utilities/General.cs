/**
 * File Name: General.cs
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
using System;
using Diagnostics = System.Diagnostics;

namespace Kokowolo.Utilities
{
    public static class General
    {
        #region Mouse Raytracing Functions

        public static bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        [Obsolete("MouseScreenPointToRay is deprecated, please use MouseScreenPointToRaycastHit instead.")]
        public static Ray MouseScreenPointToRay(Camera camera = null)
        {
            if (!camera) camera = Camera.main;
            return camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        public static bool MouseScreenPointToRaycastHit(out RaycastHit hit, LayerMask layerMask, 
            float maxDistance = Mathf.Infinity, bool debugDrawLine = false)
        {
            // Get MouseScreenPointToRay
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            bool valid = Physics.Raycast(ray, out hit, maxDistance, layerMask);
            if (debugDrawLine) Debug.DrawLine(ray.origin, hit.point, Color.white, 1f);
            return valid;
        }

        #endregion

        #region Other Funtions

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

        #endregion
    }
}