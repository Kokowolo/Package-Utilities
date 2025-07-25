/**
 * Authors: Will Lacey
 * Date Created: January 15, 2022
 * 
 * Additional Comments: 
 *      File Line Length: ~140
 *      
 *      This script has also been created in Project-Fort; although it has been adapted to better fit this package
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityRandom = UnityEngine.Random;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// A static class that contains various mathematical utility functions
    /// </summary>
    public static class KokoMath
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static int Mod(int dividend, int divisor)
        {
            // NOTE: dividend % divisor yields an incorrect result when the dividend is negative; this method corrects that
            return (dividend % divisor + divisor) % divisor;
        }

        public static int WrapClamp(int value, int minInclusive, int maxExclusive)
        {
            return (int) WrapClamp((float) value, (float) minInclusive, (float) maxExclusive);
        }

        public static float WrapClamp(float value, float minInclusive, float maxExclusive)
        {
            float range = maxExclusive - minInclusive;
            return ((value - minInclusive) % range + range) % range + minInclusive;
        }

        public static float WrapClamp01(float value)
        {
            return WrapClamp(value, 0, 1);
        }

        public static int RoundUp(float value)
        {
            return (int)Mathf.Ceil(value);
        }

        public static float Remap(float value, Vector2 from, Vector2 to)
        {
            return Remap(value, from.x, from.y, to.x, to.y);
        }

        public static float Remap(float value, float from1, float from2, float to1, float to2)
        {
            // value = Mathf.Clamp(value, Mathf.Min(from1, from2), Mathf.Max(from1, from2));
            // return to1 + (value - from1) * (to2 - to1) / Mathf.Max((from2 - from1), Mathf.Epsilon);
            return Mathf.Lerp(to1, to2, Mathf.InverseLerp(from1, from2, value));
        }

        public static float RemapTo01(float value, float from1, float from2)
        {
            return Remap(value, from1, from2, 0, 1);
        }

        public static float RemapTo01(float value, Vector2 from)
        {
            return Remap(value, from.x, from.y, 0, 1);
        }

        public static float RemapFrom01(float value, float to1, float to2)
        {
            return Remap(value, 0, 1, to1, to2);
        }

        public static float RemapFrom01(float value, Vector2 to)
        {
            return Remap(value, 0, 1, to.x, to.y);
        }

        public static int GetGreatestCommonFactor(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }
            return a | b;
        }

        public static Vector3 GetPointOnCircle(float radius, Vector3 normal, float t)
        {
            t = RemapFrom01(t, 0, 2 * Mathf.PI);
            float x = radius * Mathf.Cos(t);
            float y = radius * -Mathf.Sin(t);
            float z = 0;

            Vector3 circlePoint = new Vector3(x, y, z);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, normal);

            return rotation * circlePoint;
        }

        public static float GetRadiansOnUnitCircle(float x, float y)
        {
            float angle = Mathf.Atan2(y, x);
            if (angle < 0f) return angle + 2f * Mathf.PI;
            else return angle;
        }

        public static float DotInt(Vector3 a, Vector3 b)
        {
            return Vector3.Dot(a.normalized, b.normalized) switch
            {
                > 0.5f => 1,
                < -0.5f => -1, // TODO: this could be Math.Round() right?
                _ => 0
            };
        }

        /// <summary>
        /// Gets the relative Euler angles between two quaternions across X, Y, and Z dimensions
        /// </summary>
        public static Vector3 RelativeSignedEulerAngles(Quaternion q1, Quaternion q2)
        {
            float xAngle = RelativeSignedAngle(q1, q2, Vector3.right, Vector3.up);
            float yAngle = RelativeSignedAngle(q1, q2, Vector3.up, Vector3.right);
            float zAngle = RelativeSignedAngle(q1, q2, Vector3.forward, Vector3.up);

            return new Vector3(xAngle, yAngle, zAngle);
        }

        /// <summary>
        /// Gets the relative Euler angle between two quaternions in regards to the given axes
        /// </summary>
        public static float RelativeSignedAngle(Quaternion q1, Quaternion q2, Vector3 axis1, Vector3 axis2)
        {
            Vector3 q1Axis1 = q1 * axis1; // q1's relative axis1
            Vector3 q1Axis2 = q1 * axis2; // q1's relative axis2
            Vector3 q2Axis1 = q2 * axis1; // q2's relative axis1

            // align q2's axis1 to q1's
            Quaternion q2Axis1Aligned = Quaternion.FromToRotation(q2Axis1, q1Axis1) * q2;

            // axis2 of the q2 in world space after aligning the axis1 of q2 with the axis1 of q1
            Vector3 q2Axis2Aligned = q2Axis1Aligned * axis2;

            // gets relative signed angle
            return Vector3.SignedAngle(q1Axis2, q2Axis2Aligned, q1Axis1);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}