/*
 * File Name: Singleton.cs
 * Description: This script is for generalizing the implementation of MonoBehaviour Singletons
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 28, 2022
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class Singleton
    {
        /************************************************************/
        #region Functions

        public static T Get<T>(
            bool findObjectOfType = false, 
            bool dontDestroyOnLoad = false, 
            bool unparentGameObject = false) where T : MonoBehaviour
        {
            if (findObjectOfType && SingletonInstance<T>.instance == null) 
            {
                TrySet(Object.FindObjectOfType<T>(), dontDestroyOnLoad);
                LogManager.Log($"called Get<{typeof(T)}>() before instance was set; calling FindObjectOfType<{typeof(T)}>");
            }
            return SingletonInstance<T>.instance;
        }

        public static bool TrySet<T>(
            T instance, 
            bool dontDestroyOnLoad = false, 
            bool unparentGameObject = false) where T : MonoBehaviour
        {
            // NOTE: method does not need to be called; BUT if called, FindObjectOfType() is avoided during lazy init
            if (SingletonInstance<T>.instance != null)
            {
                LogManager.Log($"{instance.name} called Set<{typeof(T)}>() when singleton already exists");
                if (!IsSingleton(instance)) 
                {
                    LogManager.Log($"there are two different singleton instances, calling DestroyImmediate for {instance.name}");
                    Object.DestroyImmediate(instance.gameObject);
                }
                return false;
            }
            else if (instance != null)
            {
                SingletonInstance<T>.instance = instance;
                if (unparentGameObject) instance.transform.SetParent(null);
                if (dontDestroyOnLoad) Object.DontDestroyOnLoad(instance.gameObject);
                return true;
            }
            else
            {
                LogManager.LogError($"called Set<{typeof(T)}>() when given instance is null");
                return false;
            }
        }

        public static bool IsSingleton<T>(T instance) where T : MonoBehaviour
        {
            return ReferenceEquals(SingletonInstance<T>.instance, instance);
        }

        #endregion
        /************************************************************/
        #region Subclasses

        private static class SingletonInstance<T> where T : MonoBehaviour
        {
            public static T instance;
        }

        #endregion
        /************************************************************/
    }
}