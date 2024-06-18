/*
 * File Name: Vector3IntUtils.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 18, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class Vector3IntUtils
    {
        /************************************************************/
        #region Functions

        public static Vector3Int GetDirection(Vector3Int from, Vector3Int to)
        {
            return to - from;
        }

        #endregion
        /************************************************************/
    }
}