/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 24, 2023
 * 
 * Additional Comments:
 *		File Line Length: ~140
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
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        static List<string> singletonInstanceNames = new List<string>();

        [Tooltip("whether only one DontDestroyOnLoadScript GameObject can exist with this name")]
        [SerializeField] private bool willHaveSingletonInstance = true;
        [Tooltip("whether this GameObject unparents itself")]
        [SerializeField] private bool unparentGameObject;

        bool isSingletonInstance = false;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        void Awake()
        {
            if (willHaveSingletonInstance && HasSingletonInstanceWithName(name)) 
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if (isSingletonInstance) 
            {
                singletonInstanceNames.Remove(name);
            }
        }

        void Start()
        {
            if (unparentGameObject) transform.SetParent(null);
            DontDestroyOnLoad(this);

            if (willHaveSingletonInstance) 
            {
                singletonInstanceNames.Add(name);
                isSingletonInstance = true;
            }
        }

        static bool HasSingletonInstanceWithName(string name)
        {
            return singletonInstanceNames.Contains(name);
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}