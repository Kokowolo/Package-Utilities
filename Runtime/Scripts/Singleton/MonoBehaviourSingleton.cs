/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: February 16, 2023
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Class specifically for the implementation of MonoBehaviour Singletons through abstraction
    /// </summary>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Header("Settings: Singleton")]
        [Tooltip("whether Object.DontDestroyOnLoad() is called on this")]
        [SerializeField] protected bool dontDestroyOnLoad;
        [Tooltip("whether this GameObject unparents itself")]
        [SerializeField] protected bool unparentGameObject;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        protected virtual T _Instance => this as T; // NOTE: needed because `this` is MonoBehaviourSingleton<T> where `T` is a different type;
        public static T Instance => Singleton.Get<T>();

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        protected void Awake() 
        {
            Singleton.TrySet(_Instance, dontDestroyOnLoad, unparentGameObject);
            if (Singleton.IsSingleton(_Instance))
            {
                Singleton_Awake();
            }
        }

        protected void OnDestroy()
        {
            if (Singleton.IsSingleton(_Instance))
            {
                Singleton_OnDestroy();
            }
        }

        protected void OnEnable() 
        {
            if (Singleton.IsSingleton(_Instance))
            {
                Singleton_OnEnable();
            }
        }

        protected void OnDisable() 
        {
            if (Singleton.IsSingleton(_Instance))
            {
                Singleton_OnDisable();
            }
        }
        
        protected virtual void Singleton_Awake() {}
        protected virtual void Singleton_OnDestroy() {}
        
        protected virtual void Singleton_OnEnable() {}
        protected virtual void Singleton_OnDisable() {}

        public static T FindInstance() => Singleton.Get<T>(findAnyObjectByType: true);

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}