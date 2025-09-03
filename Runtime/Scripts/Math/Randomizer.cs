/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 31, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 *      TODO: add multithreading and multiple Randomizer activation/deactivation calls per frame
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    [System.Serializable]
    public class Randomizer : ISerializationCallbackReceiver
    {
        /*██████████████████████████████████████████████████████████*/
        #region Events

        public event System.Action OnSeedSet;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        static Random.State cachedState;

        const int SeedMinValue = 1;
        const int SeedMaxValue = int.MaxValue;

        [Tooltip("Seed for the randomizer; smallest possible value means random seed")]
        [SerializeField] int seed = SeedMinValue - 1;
        [SerializeField, ReadOnly] Random.State state;
        
        bool init;
        bool activated;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public int Seed 
        {
            get => seed;
            set
            {
                seed = value;
                init = false;
                TryInit();
            }
        }

        public Random.State State 
        {
            get => state;
            private set => state = value;
        }

        /*——————————————————————————————————————————————————————————*/
        #region UnityEngine.Random Properties

        /// <summary>
        /// Returns a random float within [0.0..1.0] (range is inclusive) (Read Only).
        /// </summary>
        public float value
        {
            get
            {
                if (activated) return Random.value;
                Activate();
                float value = Random.value;
                Deactivate();
                return value;
            }
        }

        /// <summary>
        /// Returns a random point inside or on a sphere with radius 1.0 (Read Only).
        /// </summary>
        public Vector3 insideUnitSphere
        {
            get
            {
                if (activated) return Random.insideUnitSphere;
                Activate();
                Vector3 value = Random.insideUnitSphere;
                Deactivate();
                return value;
            }
        }

        /// <summary>
        /// Returns a random point inside or on a circle with radius 1.0 (Read Only).
        /// </summary>
        public Vector2 insideUnitCircle
        {
            get
            {
                if (activated) return Random.insideUnitCircle;
                Activate();
                Vector2 value = Random.insideUnitCircle;
                Deactivate();
                return value;
            }
        }

        /// <summary>
        /// Returns a random point on the surface of a sphere with radius 1.0 (Read Only).
        /// </summary>
        public Vector3 onUnitSphere
        {
            get
            {
                if (activated) return Random.onUnitSphere;
                Activate();
                Vector3 value = Random.onUnitSphere;
                Deactivate();
                return value;
            }
        }

        /// <summary>
        /// Returns a random rotation (Read Only).
        /// </summary>
        public Quaternion rotation
        {
            get
            {
                if (activated) return Random.rotation;
                Activate();
                Quaternion value = Random.rotation;
                Deactivate();
                return value;
            }
        }

        /// <summary>
        /// Returns a random rotation with uniform distribution (Read Only).
        /// </summary>
        public Quaternion rotationUniform
        {
            get
            {
                if (activated) return Random.rotation;
                Activate();
                Quaternion value = Random.rotation;
                Deactivate();
                return value;
            }
        }
        
        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public Randomizer() {}
        public Randomizer(int seed)
        {
            Seed = seed;
        }

        /// <summary>
        /// Called whenever the Seed property is set or on the Randomizer's first Activate() call
        /// </summary>
        void TryInit()
        {
            // Check if already init
            if (init) return;
            init = true;
            activated = true;

            // Assign new seed
            seed = Mathf.Clamp(seed, SeedMinValue - 1, SeedMaxValue);
            if (seed == SeedMinValue - 1) 
            {
                seed = (int) System.DateTime.Now.Ticks; // NOTE: https://docs.unity3d.com/ScriptReference/Random.InitState.html
                seed = KokoMath.WrapClamp(seed, SeedMinValue, SeedMaxValue); 
            }

            // Store random state
            cachedState = Random.state;
            Random.InitState(seed);
            State = Random.state;
            Deactivate();
            OnSeedSet?.Invoke();
        }

        public void Deactivate()
        {
            if (!activated) return;
            activated = false;
            Random.state = cachedState;
        }

        public void Activate() 
        {
            TryInit();
            if (activated) return;
            activated = true;
            cachedState = Random.state;
            Random.state = State;
        }

        public void GenerateNewSeed()
        {
            Seed = SeedMinValue - 1;
        }

        /*——————————————————————————————————————————————————————————*/
        #region UnityEngine.Random Functions

        /// <summary>
        /// Returns a random float within [minInclusive..maxInclusive] (range is inclusive).
        /// </summary>
        public float Range(float minInclusive, float maxInclusive)
        {
            if (activated) return Random.Range(minInclusive, maxInclusive);
            Activate();
            float value = Random.Range(minInclusive, maxInclusive);
            Deactivate();
            return value;
        }

        /// <summary>
        /// Return a random int within [minInclusive..maxExclusive) (Read Only).
        /// </summary>
        public int Range(int minInclusive, int maxExclusive)
        {
            if (activated) return Random.Range(minInclusive, maxExclusive);
            Activate();
            int value = Random.Range(minInclusive, maxExclusive);
            Deactivate();
            return value;
        }

        /// <summary>
        /// Generates a random color from HSV and alpha ranges.
        /// </summary>
        public Color ColorHSV()
        {
            if (activated) return Random.ColorHSV();
            Activate();
            Color value = Random.ColorHSV();
            Deactivate();
            return value;
        }

        /// <summary>
        /// Generates a random color from HSV and alpha ranges.
        /// </summary>
        public Color ColorHSV(float hueMin, float hueMax)
        {
            if (activated) return Random.ColorHSV(hueMin, hueMax);
            Activate();
            Color value = Random.ColorHSV(hueMin, hueMax);
            Deactivate();
            return value;
        }

        /// <summary>
        /// Generates a random color from HSV and alpha ranges.
        /// </summary>
        public Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
        {
            if (activated) return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);
            Activate();
            Color value = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);
            Deactivate();
            return value;
        }

        /// <summary>
        /// Generates a random color from HSV and alpha ranges.
        /// </summary>
        public Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
        {
            if (activated) return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
            Activate();
            Color value = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
            Deactivate();
            return value;
        }

        /// <summary>
        /// Generates a random color from HSV and alpha ranges.
        /// </summary>
        public Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
        {
            if (activated) return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
            Activate();
            Color value = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
            Deactivate();
            return value;
        }
        
        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Editor
#if UNITY_EDITOR

        public void OnBeforeSerialize()
        {
            int s = Mathf.Clamp(seed, SeedMinValue - 1, SeedMaxValue);
            if (s == seed) return;
            seed = s;
        }

        public void OnAfterDeserialize()
        {
            // nada
        }

#endif
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}