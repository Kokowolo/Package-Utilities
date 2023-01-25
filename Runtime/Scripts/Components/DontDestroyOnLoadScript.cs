/*
 * File Name: DontDestroyOnLoadScript.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 24, 2023
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class DontDestroyOnLoadScript : MonoBehaviour
    {
        /************************************************************/
        #region Functions

        private void Start()
        {
            DontDestroyOnLoad(this);
        }
        
        #endregion
        /************************************************************/
    }
}