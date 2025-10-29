/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: May 13, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Rotates a GameObject
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [Tooltip("speed to rotate transform's euler angles")]
        [SerializeField] public Vector3 speed = new Vector3(0, 50, 0);

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}