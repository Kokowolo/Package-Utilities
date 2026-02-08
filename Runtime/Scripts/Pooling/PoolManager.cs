/* 
 * Copyright (c) 2026 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: February 6, 2026
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
#if UNITY_EDITOR
    public class PoolManager : MonoBehaviourSingleton<PoolManager>, ISerializationCallbackReceiver
#else
    public class PoolManager : MonoBehaviourSingleton<PoolManager>
#endif
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [SerializeField] List<MonoBehaviour> registeredPoolablePrefabs = new List<MonoBehaviour>();

        // Dictionary<System.Type, MonoBehaviour> pool;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static TMonoPoolable Get<TMonoPoolable>() where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour
        {
            TMonoPoolable poolable;
            if (GetCount<TMonoPoolable>() == 0) 
            {
                poolable = Instantiate(Instance.GetPrefab<TMonoPoolable>());
            }
            else 
            {
                poolable = PoolManagerStack<TMonoPoolable>.stack.Pop();
            }
            poolable.OnRemovedFromPool();
            return poolable;
        }

        public static void Add<TMonoPoolable>(TMonoPoolable poolable) where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour => Release(ref poolable);
        public static void Release<TMonoPoolable>(ref TMonoPoolable poolable) where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour
        {
            poolable.OnAddedToPool();
            PoolManagerStack<TMonoPoolable>.stack.Push(poolable);
            poolable = null;
        }

        public static bool HasAny<TMonoPoolable>() where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour => GetCount<TMonoPoolable>() > 0;
        public static int GetCount<TMonoPoolable>() where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour
        {
            return PoolManagerStack<TMonoPoolable>.stack.Count;
        }
        
        public static void Clear()
        {
            // for (int i = PoolManagerStack<TMonoPoolable>.stack.Count - 1; i >= 0 ; i--)
            // {
            //     Destroy(PoolManagerStack<TMonoPoolable>.stack.Pop());
            // }
        }
        
        public static void Clear<TMonoPoolable>() where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour
        {
            for (int i = PoolManagerStack<TMonoPoolable>.stack.Count - 1; i >= 0 ; i--)
            {
                Destroy(PoolManagerStack<TMonoPoolable>.stack.Pop());
            }
        }

        // UNDONE: should this be moved to PrefabManager?
        TMonoPoolable GetPrefab<TMonoPoolable>() where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour
        {
            if (PoolManagerStack<TMonoPoolable>.prefab == null)
            {
                for (int i = 0; i < registeredPoolablePrefabs.Count; i++)
                {
                    if (registeredPoolablePrefabs[i].TryGetComponent(out TMonoPoolable prefab))
                    {
                        PoolManagerStack<TMonoPoolable>.prefab = prefab;
                    }
                }
            }
            return PoolManagerStack<TMonoPoolable>.prefab;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Subclasses
        
        static class PoolManagerStack<TMonoPoolable> where TMonoPoolable : MonoBehaviour, IPoolableMonoBehaviour
        {
            public static TMonoPoolable prefab;
            public static Stack<TMonoPoolable> stack = new Stack<TMonoPoolable>();
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Editor
#if UNITY_EDITOR

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // // LogManager.Log($"{nameof(OnBeforeSerialize)}");
            // for (int i = registeredPoolablePrefabs.Count - 1; i >= 0; i--)
            // {
            //     // LogManager.Log(poolableObjects[i].name);
            //     if (registeredPoolablePrefabs[i] != null && registeredPoolablePrefabs[i] is IPoolableMonoBehaviour poolableObject)
            //     {
            //         // poolableObjects.RemoveAt(i);
            //         LogManager.Log(poolableObject);
            //     }
            //     else if (registeredPoolablePrefabs[i] != null)
            //     {
            //         LogManager.LogWarning($"{registeredPoolablePrefabs[i].name} is not poolable");
            //     }
            // }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            // nada
        }

#endif
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}