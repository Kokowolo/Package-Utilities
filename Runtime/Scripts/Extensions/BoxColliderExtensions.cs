/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: November 24, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities
{
    public static class BoxColliderExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Enums

        [Flags]
        public enum BoxLocation
        {
            None    = 0,
            Center  = 1,
            Pivot   = 2,
            XPos    = 4,
            XNeg    = 8,
            YPos    = 16,
            YNeg    = 32,
            ZPos    = 64,
            ZNeg    = 128,
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static Vector3 GetPosition(this BoxCollider box, BoxLocation fromLocation)
        {
            Vector3 center = box.bounds.center;
            switch (fromLocation)
            {
                case BoxLocation.XNeg:
                case BoxLocation.XPos:
                {
                    center.x += (fromLocation == BoxLocation.XPos ? 0.5f : -0.5f) * box.bounds.size.x;
                    return center;
                }
                case BoxLocation.YNeg:
                case BoxLocation.YPos:
                {
                    center.y += (fromLocation == BoxLocation.YPos ? 0.5f : -0.5f) * box.bounds.size.y;
                    return center;
                }
                case BoxLocation.ZNeg:
                case BoxLocation.ZPos:
                {
                    center.z += (fromLocation == BoxLocation.ZPos ? 0.5f : -0.5f) * box.bounds.size.z;
                    return center;
                }
                case BoxLocation.Center:
                {
                    return center;
                }
                case BoxLocation.Pivot:
                {
                    return box.transform.position;
                }
            }
            throw new System.Exception($"{nameof(BoxCollider)} {fromLocation} is not recognized");
        }

        public static void SetSize(this BoxCollider box, float value, BoxLocation fromLocation = BoxLocation.Center)
        {
            Vector3 size = box.bounds.size;
            if (fromLocation == BoxLocation.XPos || fromLocation == BoxLocation.XNeg) size.x = value;
            if (fromLocation == BoxLocation.YPos || fromLocation == BoxLocation.YNeg) size.y = value;
            if (fromLocation == BoxLocation.ZPos || fromLocation == BoxLocation.ZNeg) size.z = value;
            SetSize(box, size, fromLocation);
        }
        
        public static void SetSize(this BoxCollider box, Vector3 size, BoxLocation fromLocation)
        {
            Bounds bounds = box.bounds;
            Vector3 position = GetPosition(box, fromLocation);
            // 
            // Vector3 center = bounds.center;
            switch (fromLocation)
            {
                case BoxLocation.XNeg:
                case BoxLocation.XPos:
                {
                    position.x += (fromLocation == BoxLocation.XPos ? -0.5f : 0.5f) * size.x;
                    break;
                }
                case BoxLocation.YNeg:
                case BoxLocation.YPos:
                {
                    position.y += (fromLocation == BoxLocation.YPos ? -0.5f : 0.5f) * size.y;
                    break;
                }
                case BoxLocation.ZNeg:
                case BoxLocation.ZPos:
                {
                    position.z += (fromLocation == BoxLocation.ZPos ? -0.5f : 0.5f) * size.z;
                    break;
                }
                case BoxLocation.Center:
                {
                    break; // nada
                }
                case BoxLocation.Pivot:
                {
                    // TODO: [HAT-103] Collider Resize From Pivot - add relative bounding box setting
                    throw new System.Exception($"SetSize not supported from {fromLocation}");
                }
                default:
                {
                    throw new System.Exception($"SetSize not supported from {fromLocation}");
                }
            }
            bounds.center = position;
            bounds.size = 2 * new Vector3(size.x, size.y, size.z);
            SetBounds(box, bounds);
        }

        public static void SetBounds(this BoxCollider box, Bounds bounds)
        {
            box.center = box.transform.InverseTransformPoint(bounds.center);
            box.size = bounds.extents;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}