/*
 * File Name: PrefabManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: August 22, 2022
 * 
 * Additional Comments:
 *      File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities
{
    [DefaultExecutionOrder(-100)]
    public class PrefabManager : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [Header("Singleton Settings")]
        [SerializeField] private bool dontDestroyOnLoad = false;

        [Header("Cached References")]
        // [SerializeField] private GameObject[] gameObjects = null;
        [SerializeField] private MonoBehaviourContainer[] monoBehaviours = null;
        [SerializeField] private ScriptableObjectContainer[] scriptableObjects = null;

        #endregion
        /************************************************************/
        #region Properties

        private static PrefabManager Instance => Singleton.Get<PrefabManager>(findObjectOfType: true);

        #endregion
        /************************************************************/
        #region Functions

        private void Awake() => Singleton.TrySet(this, dontDestroyOnLoad);

        public static bool Has<T>()
        {
            return Prefab<T>.prefab != null;
        }

        public static T Get<T>()
        {
            if (!Has<T>()) InitPrefab<T>();
            return Prefab<T>.prefab;
        }

        public static void InitPrefab<T>()
        {
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour))) 
            {
                Instance.InitMonoBehaviourPrefab<T>();
            }
            else if (typeof(T).IsSubclassOf(typeof(ScriptableObject))) 
            {
                Instance.InitScriptableObjectPrefab<T>();
            }
            // else if (typeof(T).IsSubclassOf(typeof(GameObject))) 
            // {
            //     Instance.InitGameObjectPrefab<T>();
            // }
            else
            {
                throw new Exception($"[PrefabManager] Type {typeof(T)} must extend from UnityEngine.Object");
            }
        }

        private void InitMonoBehaviourPrefab<T>()
        {
            foreach (MonoBehaviourContainer container in monoBehaviours)
            {
                foreach (MonoBehaviour monoBehaviour in container.monoBehaviours)
                {
                    if (monoBehaviour.TryGetComponent<T>(out T prefab)) 
                    {
                        Prefab<T>.prefab = prefab;
                    }
                }
            }
        }

        private void InitScriptableObjectPrefab<T>()
        {
            foreach (ScriptableObjectContainer container in scriptableObjects)
            {
                foreach (ScriptableObject scriptableObject in container.scriptableObjects)
                {
                    if (scriptableObject.GetType() == typeof(T)) 
                    {
                        Prefab<T>.prefab = (T) Convert.ChangeType(scriptableObject, typeof(T));
                    }
                }
            }
        }

        #endregion
        /************************************************************/
        #region Subclasses

        [Serializable]
        private class MonoBehaviourContainer
        {
            [SerializeField] public string description;
            [SerializeField] public MonoBehaviour[] monoBehaviours;
        }

        [Serializable]
        private class ScriptableObjectContainer
        {
            [SerializeField] public string description;
            [SerializeField] public ScriptableObject[] scriptableObjects;
        }

        private static class Prefab<T>
        {
            public static T prefab;
        }

        #endregion
        /************************************************************/
    }
}

