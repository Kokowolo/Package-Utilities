/*
 * File Name: PoolSystem.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 11, 2023
 * 
 * Additional Comments:
 *      File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;

namespace Kokowolo.Utilities
{
    public class PoolSystem : MonoSingleton<PoolSystem>
    {
        /************************************************************/
        #region Functions

        public static int GetCount<T>()
        {
            return PoolSystemStack<T>.stack.Count;
        }

        public static void Add<T>(T poolable) where T : IPoolable<T>
        {
            Add(poolable, null);
        }

        public static void Add<T>(T poolable, params object[] args) where T : IPoolable<T>
        {
            poolable.OnAddPoolable(args);
            PoolSystemStack<T>.stack.Push(poolable);
        }

        public static T Get<T>() where T : IPoolable<T>
        {
            T poolable;
            if (GetCount<T>() == 0) 
            {
                poolable = CreateIPoolable<T>();
            }
            else 
            {
                poolable = PoolSystemStack<T>.stack.Pop();
                poolable.OnGetPoolable();
                
            }
            return poolable;
        }

        private static T CreateIPoolable<T>() where T : IPoolable<T>
        {
            // NOTE: This is a hacky way to get around the fact that you can't call a static method on a generic type
            MethodInfo createMethod = typeof(T).GetMethod("Create", BindingFlags.Public | BindingFlags.Static);
            if (createMethod == null || createMethod.ReturnType != typeof(T))
            {
                throw new System.Exception($"no static Create method found within type {nameof(T)}");
            }
            return (T)createMethod.Invoke(null, null);
        }
        
        #endregion
        /************************************************************/
        #region Subclasses

        private static class PoolSystemStack<T>
        {
            public static Stack<T> stack = new Stack<T>();
        }

        #endregion
        /************************************************************/
    }
}