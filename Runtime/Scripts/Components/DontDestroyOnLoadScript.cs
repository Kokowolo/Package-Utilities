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
        [SerializeField] private bool isSingletonInstance = true;

        private static List<string> singletonInstanceNames = new List<string>();

        #endregion
        /************************************************************/
        #region Functions

        private void Awake()
        {
            if (isSingletonInstance && HasSingletonInstanceWithName(name)) 
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            if (isSingletonInstance) singletonInstanceNames.Add(name);
        }

        private static bool HasSingletonInstanceWithName(string name)
        {
            return singletonInstanceNames.Contains(name);
        }
        
        #endregion
        /************************************************************/
    }
}