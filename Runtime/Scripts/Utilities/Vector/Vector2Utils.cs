/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 18, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class Vector2Utils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            return to - from;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}