/*
 * File Name: MonoSingleton.cs
 * Description: This script is specifically for the implementation of MonoBehaviour Singletons through abstraction
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: February 16, 2023
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [Header("Singleton Settings")]
        [Tooltip("whether Object.DontDestroyOnLoad() is called on this")]
        [SerializeField] protected bool dontDestroyOnLoad;
        [Tooltip("whether this GameObject unparents itself")]
        [SerializeField] protected bool unparentGameObject;

        #endregion
        /************************************************************/
        #region Properties

        private T _Instance => this as T;
        public static T Instance => Singleton.Get<T>();

        #endregion
        /************************************************************/
        #region Functions

        protected void Awake() 
        {
            if (Singleton.TrySet(_Instance, dontDestroyOnLoad, unparentGameObject))
            {
                MonoSingleton_Awake();
            }
        }
        
        protected void OnDestroy()
        {
            if (Singleton.IsSingleton(_Instance))
            {
                MonoSingleton_OnDestroy();
            }
        }

        protected void OnEnable() 
        {
            if (Singleton.IsSingleton(_Instance))
            {
                MonoSingleton_OnEnable();
            }
        }
        
        protected void OnDisable() 
        {
            if (Singleton.IsSingleton(_Instance))
            {
                MonoSingleton_OnDisable();
            }
        }
        
        protected virtual void MonoSingleton_Awake() {}
        
        protected virtual void MonoSingleton_OnEnable() {}

        protected virtual void MonoSingleton_OnDisable() {}

        protected virtual void MonoSingleton_OnDestroy() {}

        public static T FindInstance() => Singleton.Get<T>(findObjectOfType: true);

        #endregion
        /************************************************************/
    }
}