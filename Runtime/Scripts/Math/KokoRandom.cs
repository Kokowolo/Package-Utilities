/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 03, 2025
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace Kokowolo.Utilities
{
    public static class KokoRandom
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        static Texture2D _noiseSource;

        #endregion
        /*██████████████████████████████████████████████████████████*/
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
        /*██████████████████████████████████████████████████████████*/
        #region Properties

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

        /// <summary>
        /// Evaluates if `t`, the chance of success, is successful
        /// </summary>
        /// <param name="t">chance of success value between 0f and 1f</param>
        /// <returns>whether success was achieved</returns>
        public static bool TryChanceOfSuccess(float t)
        {
            if (t < 0f || t > 1f) Debug.LogWarning($"{nameof(TryChanceOfSuccess)}() only takes a value between 0f and 1f"); 
            return UnityRandom.Range(0f, 1f) <= t;
        }

        public static Vector3 Vector3(float range) => Vector3(-range, range);
        public static Vector3 Vector3(float min, float max)
        {
            return new Vector3(UnityRandom.Range(min, max), UnityRandom.Range(min, max), UnityRandom.Range(min, max));
        }
        
        public static Color Color()
        {
            return new Color(UnityRandom.Range(0f, 1f), UnityRandom.Range(0f, 1f), UnityRandom.Range(0f, 1f));
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}
