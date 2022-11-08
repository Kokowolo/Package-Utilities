/*
 * File Name: Singleton.cs
 * Description: This script is for generalizing the implementation of MonoBehaviour Singletons
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 28, 2022
 * 
 * Additional Comments:
 *      File Line Length: 120
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

        public static T Get(bool findObjectOfType = true, bool dontDestroyOnLoad = true)
        {
            if (findObjectOfType && !instance) 
            {
                Set(FindObjectOfType<T>(), dontDestroyOnLoad);
                Debug.LogWarning($"[Singleton] called Get<{typeof(T)}>() before instance was set; calling FindObjectOfType<{typeof(T)}>");
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
            else if (instance)
            {
                Singleton<T>.instance = instance;
                if (dontDestroyOnLoad) DontDestroyOnLoad(instance.gameObject);
                return true;
            }
            else
            {
                Debug.LogError($"[Singleton] called Set<{typeof(T)}>() but given instance is null");
                return false;
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