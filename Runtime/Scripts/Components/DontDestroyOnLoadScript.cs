/*
 * File Name: DontDestroyOnLoadScript.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 24, 2023
 * 
 * Additional Comments:
 *		File Line Length: 120
 * 
 *      NOTE: DontDestroyOnLoadScript requires the MonoBehaviour's GameObject to have a unique name
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class DontDestroyOnLoadScript : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [Tooltip("whether only one DontDestroyOnLoadScript GameObject can exist with this name")]
        [SerializeField] private bool willHaveSingletonInstance = true;
        [Tooltip("whether this GameObject unparents itself")]
        [SerializeField] private bool unparentGameObject;

        private bool isSingletonInstance = false;

        private static List<string> singletonInstanceNames = new List<string>();

        #endregion
        /************************************************************/
        #region Functions

        private void Awake()
        {
            if (willHaveSingletonInstance && HasSingletonInstanceWithName(name)) 
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (isSingletonInstance) 
            {
                singletonInstanceNames.Remove(name);
            }
        }

        private void Start()
        {
            if (unparentGameObject) transform.SetParent(null);
            DontDestroyOnLoad(this);

            if (willHaveSingletonInstance) 
            {
                singletonInstanceNames.Add(name);
                isSingletonInstance = true;
            }
        }

        private static bool HasSingletonInstanceWithName(string name)
        {
            return singletonInstanceNames.Contains(name);
        }
        
        #endregion
        /************************************************************/
    }
}