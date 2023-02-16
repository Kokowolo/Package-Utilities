/*
 * File Name: MonoSingleton.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: February 16, 2023
 * 
 * Additional Comments:
 *		File Line Length: 120
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
        [SerializeField] private bool dontDestroyOnLoad;

        #endregion
        /************************************************************/
        #region Properties

        public static T Instance => Singleton.Get<T>();

        #endregion
        /************************************************************/
        #region Functions

        protected virtual bool Awake() 
        {
            return Singleton.TrySet(this, dontDestroyOnLoad);
        }

        protected virtual bool OnDestroy()
        {
            return Singleton.IsSingleton(this);
        }
        
        #endregion
        /************************************************************/
    }
}

