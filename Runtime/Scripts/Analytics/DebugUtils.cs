/*
 * File Name: DebugUtils.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 17, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class DebugUtils
    {
        /************************************************************/
        #region Fields

        #endregion
        /************************************************************/
        #region Properties

        #endregion
        /************************************************************/
        #region Functions

        public static void DrawBounds(float xMin, float yMin, float zMin, float sizeX, float sizeY, float sizeZ, Color color, float duration)
        {
            Vector3 center = new Vector3((xMin + sizeX) * 0.5f, (yMin + sizeY) * 0.5f, (zMin + sizeZ) * 0.5f);
            Vector3 size = new Vector3(sizeX, sizeY, sizeZ);
            Bounds bounds = new Bounds(center, size);
            DrawBounds(bounds, color, duration);
        }
        
        public static void DrawBounds(Bounds bounds, Color color, float duration)
        {
            Vector3 origin, start, end;

            // front 
            origin = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
            start = origin;
            end = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
            Debug.DrawLine(start, end, color, duration);
            start = end;
            end = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
            Debug.DrawLine(start, end, color, duration);
            start = end;
            end = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
            Debug.DrawLine(start, end, color, duration);
            start = end;
            end = origin;
            Debug.DrawLine(start, end, color, duration);

            // back 
            origin = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
            start = origin;
            end = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
            Debug.DrawLine(start, end, color, duration);
            start = end;
            end = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
            Debug.DrawLine(start, end, color, duration);
            start = end;
            end = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
            Debug.DrawLine(start, end, color, duration);
            start = end;
            end = origin;
            Debug.DrawLine(start, end, color, duration);

            // connection 
            start = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
            end = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
            Debug.DrawLine(start, end, color, duration);
            start = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
            end = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
            Debug.DrawLine(start, end, color, duration);
            start = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
            end = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
            Debug.DrawLine(start, end, color, duration);
            start = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
            end = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
            Debug.DrawLine(start, end, color, duration);
        }

        public static void DrawBounds(BoundsInt bounds, Color color, float duration)
        {
            DrawBounds(new Bounds(bounds.center, bounds.size), color, duration);
        }

        #endregion
        /************************************************************/
    }
}