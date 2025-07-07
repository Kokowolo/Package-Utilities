/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: August 22, 2022
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities
{
    [DefaultExecutionOrder(-100)]
    public class PrefabManager : MonoBehaviourSingleton<PrefabManager>
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Header("References")]
        // [SerializeField] private GameObject[] gameObjects = null;
        [SerializeField] MonoBehaviourContainer[] monoBehaviours = null;
        [SerializeField] ScriptableObjectContainer[] scriptableObjects = null;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public static new PrefabManager Instance => FindInstance();

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static T Get<T>()
        {
            if ((Prefab<T>.prefab == null)) InitPrefab<T>();
            if (Prefab<T>.prefab == null) 
            {
                Debug.LogError($"[{nameof(PrefabManager)}] No prefab of type, {typeof(T)}, found");
            }
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
                throw new Exception($"[{nameof(PrefabManager)}] Type {typeof(T)} must extend from UnityEngine.Object");
            }
        }

        void InitMonoBehaviourPrefab<T>()
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

        void InitScriptableObjectPrefab<T>()
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
        /*██████████████████████████████████████████████████████████*/
        #region Subclasses

        [Serializable]
        class MonoBehaviourContainer
        {
            [SerializeField] public string description;
            [SerializeField] public MonoBehaviour[] monoBehaviours;
        }

        [Serializable]
        class ScriptableObjectContainer
        {
            [SerializeField] public string description;
            [SerializeField] public ScriptableObject[] scriptableObjects;
        }

        static class Prefab<T>
        {
            public static T prefab;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}

