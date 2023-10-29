/*
 * File Name: Tween.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 26, 2023
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities
{
    public static class Tween
    {
        /************************************************************/
        #region Fields

        #endregion
        /************************************************************/
        #region Properties

        #endregion
        /************************************************************/
        #region Functions

        public static ScheduledEvent Loglerp(float value, float a, float b, float speed, Action<float> setFunction)
        {
            return ScheduledEventManager.StartEvent(LoglerpRoutine(value, a, b, speed, setFunction));
        }

        private static IEnumerator LoglerpRoutine(float value, float a, float b, float speed, Action<float> setFunction)
        {
            float t = MathKoko.Remap01(value, a, b);
            while (t < 1)
            {
                value = Mathf.Lerp(value, b, t);
                setFunction(value);
                t += Time.deltaTime * speed;
                yield return null;
            }
            setFunction(b);
        }

        public static ScheduledEvent Lerp(float value, float a, float b, float speed, Action<float> setFunction)
        {
            return ScheduledEventManager.StartEvent(LerpRoutine(value, a, b, speed, setFunction));
        }

        private static IEnumerator LerpRoutine(float value, float a, float b, float speed, Action<float> setFunction)
        {
            float t = MathKoko.Remap01(value, a, b);
            while (t < 1)
            {
                value = Mathf.Lerp(a, b, t);
                setFunction(value);
                t += Time.deltaTime * speed;
                yield return null;
            }
            setFunction(b);
        }
        
        #endregion
        /************************************************************/
    }
}