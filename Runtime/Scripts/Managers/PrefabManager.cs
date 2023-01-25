/*
 * File Name: PrefabManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: August 22, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
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
        [SerializeField] private MonoBehaviour[] monoBehaviours = null;
        [SerializeField] private ScriptableObject[] scriptableObjects = null;

        #endregion
        /************************************************************/
        #region Properties

        private static PrefabManager Instance => Singleton.Get<PrefabManager>(findObjectOfType: false);

        #endregion
        /************************************************************/
        #region Functions

        private void Awake() => Singleton.TrySet(this, dontDestroyOnLoad);

        public static T Get<T>()
        {
            if (Prefab<T>.prefab == null) InitPrefab<T>();
            return Prefab<T>.prefab;
        }

        public static void InitPrefab<T>()
        {
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour))) 
            {
                InitMonoBehaviourPrefab<T>();
            }
            else if (typeof(T).IsSubclassOf(typeof(ScriptableObject))) 
            {
                InitScriptableObjectPrefab<T>();
            }
            // else if (typeof(T).IsSubclassOf(typeof(GameObject))) 
            // {
            //     InitGameObjectPrefab<T>();
            // }
            else
            {
                throw new Exception($"[PrefabManager] Type {typeof(T)} must extend from UnityEngine.Object");
            }
        }

        private static void InitMonoBehaviourPrefab<T>()
        {
            for (int i = 0; i < Instance.monoBehaviours.Length; i++)
            {
                if (Instance.monoBehaviours[i].TryGetComponent<T>(out T monoBehaviour)) 
                {
                    Prefab<T>.prefab = monoBehaviour;
                }
            }
        }

        private static void InitScriptableObjectPrefab<T>()
        {
            for (int i = 0; i < Instance.scriptableObjects.Length; i++)
            {
                if (Instance.scriptableObjects[i].GetType() == typeof(T)) 
                {
                    Prefab<T>.prefab = (T) Convert.ChangeType(Instance.scriptableObjects[i], typeof(T));
                }
            }
        }

        #endregion
        /************************************************************/
        #region Subclasses

        private static class Prefab<T>
        {
            public static T prefab;
        }

        #endregion
        /************************************************************/
    }
}

