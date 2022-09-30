/*
 * File Name: Rotator.cs
 * Description: This script is for rotating a transform
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 13, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class Rotator : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        [Tooltip("speed to rotate transform's euler angles")]
        [SerializeField] public Vector3 speed = new Vector3(0, 50, 0);

        #endregion
        /************************************************************/
        #region Functions

        private void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }

        #endregion
        /************************************************************/
    }
}