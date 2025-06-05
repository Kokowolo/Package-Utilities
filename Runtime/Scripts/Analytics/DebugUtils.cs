/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 17, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class DebugUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static void DrawWireBox(Vector3 center, Vector3 halfExtents, Quaternion rotation, Color color, float duration)
        {
            // create matrix
            Matrix4x4 m = new Matrix4x4();
            m.SetTRS(center, rotation, halfExtents*2);

            var point1 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, 0.5f));
            var point2 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, 0.5f));
            var point3 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, -0.5f));
            var point4 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, -0.5f));

            var point5 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, 0.5f));
            var point6 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, 0.5f));
            var point7 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, -0.5f));
            var point8 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, -0.5f));
            
            Debug.DrawLine(point1, point2, color, duration);
            Debug.DrawLine(point2, point3, color, duration);
            Debug.DrawLine(point3, point4, color, duration);
            Debug.DrawLine(point4, point1, color, duration);

            Debug.DrawLine(point5, point6, color, duration);
            Debug.DrawLine(point6, point7, color, duration);
            Debug.DrawLine(point7, point8, color, duration);
            Debug.DrawLine(point8, point5, color, duration);

            Debug.DrawLine(point1, point5, color, duration);
            Debug.DrawLine(point2, point6, color, duration);
            Debug.DrawLine(point3, point7, color, duration);
            Debug.DrawLine(point4, point8, color, duration);

            // optional axis display
            // Debug.DrawRay(m.GetPosition(), m.GetForward(), Color.magenta);
            // Debug.DrawRay(m.GetPosition(), m.GetUp(), Color.yellow);
            // Debug.DrawRay(m.GetPosition(), m.GetRight(), Color.red);
        }

        public static void DrawBounds(float xMin, float yMin, float zMin, float sizeX, float sizeY, float sizeZ, Color color, float duration)
        {
            DrawBounds(BoundsUtils.CreateBounds(xMin, yMin, zMin, sizeX, sizeY, sizeZ), color, duration);
        }

        public static void DrawBounds(Vector3 minPosition, Vector3 size, Color color, float duration)
        {
            DrawBounds(BoundsUtils.CreateBounds(minPosition, size), color, duration);
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
        /*██████████████████████████████████████████████████████████*/
    }
}