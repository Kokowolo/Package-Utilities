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
    public static class Math
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
        /// Evaluates if the odds of success is successful
        /// </summary>
        /// <param name="oddsOfSuccess">value between 0 and 1</param>
        /// <returns>whether success was achieved</returns>
        public static bool TryProbabilityOfSuccess(float oddsOfSuccess)
        {
            if (oddsOfSuccess > 1) LogManager.LogWarning("TryProbabilityOfSuccess() only takes a value between 0 and 1"); 
            return Random.Range(0, 1) <= oddsOfSuccess;
        }

        public static Vector3 Perturb(Vector3 point, float noiseStrength, bool useTime = false)
        {
            // samples the noise source for randomness using a given point, yields a random value between 0 and 1
            Vector4 sample = (useTime) ? 
                NoiseSource.GetPixelBilinear(point.x * NoiseSampleScale * Time.time, point.z * NoiseSampleScale * Time.time) :
                NoiseSource.GetPixelBilinear(point.x * NoiseSampleScale, point.z * NoiseSampleScale);

            // convert the sample to a value between -1 and 1, then multiply it by it corresponding noise strength
            point.x += (sample.x * 2f - 1f) * noiseStrength;
            point.y += (sample.y * 2f - 1f) * noiseStrength;
            point.z += (sample.z * 2f - 1f) * noiseStrength;

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

        public static float Normalize(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        public static int RoundUp(float value)
        {
            return (int)Mathf.Ceil(value);
        }

        public static float Remap(float value, float fromRangeMin, float fromRangeMax, float toRangeMin, float toRangeMax)
        {
            value = Mathf.Clamp(value, fromRangeMin, fromRangeMax); // just in case the value is outside the bounds
            return (value - fromRangeMin) / (fromRangeMax - fromRangeMin) * (toRangeMax - toRangeMin) + toRangeMin;
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