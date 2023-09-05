/*
 * File Name: Diagnostics.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 1, 2023
 * 
 * Additional Comments:
 *      File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities//.Analytics
{
    public static class Diagnostics
    {
        /************************************************************/
        #region Functions

        public static double TimeFunctionWithStopwatch(Action function, int numberOfExecutions = 1, bool logElapsedTime = true)
        {
            double totalTime = 0;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                stopwatch.Start();
                function?.Invoke();
                stopwatch.Stop();
                totalTime += stopwatch.ElapsedTicks * 1000D / System.Diagnostics.Stopwatch.Frequency;
                stopwatch.Reset();
            }
            if (logElapsedTime) LogElapsedTime(numberOfExecutions, totalTime);
            return totalTime / numberOfExecutions;
        }

        // NOTE: since Coroutine's don't have return types, this just logs the elapsed time
        public static void TimeCoroutineWithStopwatch(IEnumerator routine, int numberOfExecutions = 1)
        {
            SchedulingManager.AddAsynchronousEvent(TimeFunctionWithStopwatchCoroutine(routine, numberOfExecutions));
        }

        private static IEnumerator TimeFunctionWithStopwatchCoroutine(IEnumerator routine, int numberOfExecutions = 1)
        {
            double totalTime = 0;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                stopwatch.Start();
                yield return routine;
                stopwatch.Stop();
                totalTime += stopwatch.ElapsedTicks * 1000D / System.Diagnostics.Stopwatch.Frequency;
                stopwatch.Reset();
            }
            /*if (logElapsedTime)*/ LogElapsedTime(numberOfExecutions, totalTime);
            // return totalTime / numberOfExecutions;
        }

        private static void LogElapsedTime(int numberOfExecutions, double totalTime)
        {
            if (numberOfExecutions != 1)
            {
                LogManager.Log($"Average Execution Time: {(totalTime / numberOfExecutions).ToString("F10")} ms");
            }
            LogManager.Log($"Total Execution Time: {totalTime:F10} ms");
        }
        
        #endregion
        /************************************************************/
    }
}