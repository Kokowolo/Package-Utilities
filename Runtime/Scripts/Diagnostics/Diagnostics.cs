/*
 * File Name: Diagnostics.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 1, 2023
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities//.Diagnostics
{
    public static class Diagnostics
    {
        /************************************************************/
        #region Functions

        public static double TimeFunctionWithStopwatch(Action function, int numberOfExecutions = 1, bool logElapsedTime = true)
        {
            double averageElapsedTime = 0;
            double totalElapsedTime = 0;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                stopwatch.Start();
                function?.Invoke();
                stopwatch.Stop();
                averageElapsedTime += stopwatch.ElapsedTicks * 1000D / System.Diagnostics.Stopwatch.Frequency;
                totalElapsedTime += stopwatch.ElapsedTicks * 1000D / System.Diagnostics.Stopwatch.Frequency;
                stopwatch.Reset();
            }
            averageElapsedTime /= numberOfExecutions;

            if (logElapsedTime)
            {
                if (numberOfExecutions == 1)
                {
                    LogManager.Log($"Execution Time: {totalElapsedTime.ToString("F10")} ms");
                }
                else
                {
                    LogManager.Log($"Average Execution Time: {averageElapsedTime.ToString("F10")} ms");
                    LogManager.Log($"Total Execution Time: {totalElapsedTime.ToString("F10")} ms");
                }
            }

            return averageElapsedTime;
        }
        
        #endregion
        /************************************************************/
    }
}