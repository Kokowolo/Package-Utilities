/**
 * File Name: MathUtils.cs
 * Description: This script is a static class that contains various mathematical utility functions
 * 
 * Authors: Will Lacey
 * Date Created: January 15, 2022
 * 
 * Additional Comments: 
 *      File Line Length: 120
 *      
 *      This script has also been created in Project-Fort; although it has been adapted to better fit this package
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class MathKoko
    {
        /************************************************************/
        #region Fields

        static Texture2D _noiseSource;

        #endregion
        /************************************************************/
        #region Properties

        public static float NoiseSampleScale { get; set; } = 0.2f;
        // public static float NoiseSampleSpeed { get; set; } = 1f; TODO: [BED-7] refactor and add this to Perturb() w/ useTime

        public static Texture2D NoiseSource
        {
            get
            {
                if (!_noiseSource) _noiseSource = Resources.Load<Texture2D>("Noise Perlin");
                return _noiseSource;
            }
            set => _noiseSource = value;
        }

        #endregion
        /************************************************************/
        #region Functions

        #region Randomness Functions

        /// <summary>
        /// Evaluates if t, the chance of success, is successful
        /// </summary>
        /// <param name="t">chance of success value between 0f and 1f</param>
        /// <returns>whether success was achieved</returns>
        public static bool TryChanceOfSuccess(float t)
        {
            if (t > 1f) LogManager.LogWarning("TryChanceOfSuccess() only takes a value between 0f and 1f"); 
            return Random.Range(0f, 1f) <= t;
        }

        public static Vector3 Perturb(Vector3 point, float noiseStrength,
            bool useX = true, bool useY = true, bool useZ = true, bool useTime = false)
        {
            // samples the noise source for randomness using a given point, yields a random value between 0 and 1
            Vector4 sample = (useTime) ? 
                NoiseSource.GetPixelBilinear(point.x * NoiseSampleScale * Time.time, point.z * NoiseSampleScale * Time.time) :
                NoiseSource.GetPixelBilinear(point.x * NoiseSampleScale, point.z * NoiseSampleScale);

            // convert the sample to a value between -1 and 1, then multiply it by it corresponding noise strength
            point.x += useX ? (sample.x * 2f - 1f) * noiseStrength : 0f;
            point.y += useY ? (sample.y * 2f - 1f) * noiseStrength : 0f;
            point.z += useZ ? (sample.z * 2f - 1f) * noiseStrength : 0f;

            return point;
        }

        public static Vector3 GetRandomVector3(float minInclusive, float maxInclusive)
        {
            return new Vector3
            {
                x = Random.Range(minInclusive, maxInclusive),
                y = Random.Range(minInclusive, maxInclusive),
                z = Random.Range(minInclusive, maxInclusive)
            };
        }

        public static Color GetRandomColor()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        #endregion

        #region Other Math Functions

        public static int Mod(int dividend, int divisor)
        {
            // NOTE: dividend % divisor yields an incorrect result when the dividend is negative; this func corrects that
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

        #endregion
        /************************************************************/
    }
}