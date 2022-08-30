/*
 * File Name: Singleton.cs
 * Description: This script is for generalizing the implementation of MonoBehaviour Singletons
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 28, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using UnityEngine;
using System;

namespace Kokowolo.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        private static T instance;

        #endregion
        /************************************************************/
        #region Functions

        public static T Get()
        {
            if (!instance) 
            {
                Set(FindObjectOfType<T>());
                Debug.Log($"[Singleton] called Get<{typeof(T)}>() before instance was set");
            }
            return instance;
        }

        public static bool Set(T instance, bool dontDestroyOnLoad = true)
        {
            // NOTE: method does not need to be called; BUT if called, FindObjectOfType() is avoided during lazy init
            if (Singleton<T>.instance)
            {
                Debug.LogWarning($"[Singleton] {instance.name} called Set<{typeof(T)}>() when singleton already exists");
                if (!ReferenceEquals(Singleton<T>.instance, instance)) DestroyImmediate(instance.gameObject);
                return false;
            }
            else
            {
                Singleton<T>.instance = instance;
                if (dontDestroyOnLoad) DontDestroyOnLoad(instance.gameObject);
                return true;
            }
        }

        [Obsolete("Release() is deprecated, just destroy the singleton MonoBehaviour like any other GameObject")]
        public static void Release()
        {
            if (!instance) return;
            Destroy(instance.gameObject);
        }

        #endregion
        /************************************************************/
    }
}