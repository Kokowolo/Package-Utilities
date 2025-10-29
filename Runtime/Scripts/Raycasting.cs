/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 18, 2022
 *
 * Additional Comments:
 *		File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class Raycasting
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static bool RaycastToDestinationPoint(Vector3 origin, Vector3 destination, out RaycastHit hitInfo, LayerMask layerMask)
        {
            Vector3 direction = (destination - origin).normalized;
            float maxDistance = Vector3.Distance(origin, destination);
            return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask);
        }

        public static bool RaycastFromMouseScreenPoint(out RaycastHit hitInfo, LayerMask layerMask, 
            float maxDistance = Mathf.Infinity, Camera camera = null)
        {
            if (!camera) camera = Camera.main;
            Ray ray = camera.ScreenPointToRay(InputManager.GetMouseScreenPoint());
            if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
            {
                return true;
            }
            else
            {
                hitInfo.point = ray.origin + ray.direction * Mathf.Clamp(maxDistance, 0, 1000);
                return false;
            }
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}
