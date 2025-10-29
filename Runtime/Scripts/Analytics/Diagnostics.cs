/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 1, 2023
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities//.Analytics
{
    public static class Diagnostics
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static string GetStackTrace(int startingStackFrame = 1, bool getMethod = true)
        {
            string stackTraceString = "";
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                if (i < startingStackFrame) continue;
                System.Diagnostics.StackFrame sf = stackTrace.GetFrame(i);
                if (getMethod)
                {
                    stackTraceString += $"{sf.GetFileName()}:{sf.GetMethod()}:{sf.GetFileLineNumber()}{(i + 1 == stackTrace.FrameCount ? "" : "\n")}";
                }
                else
                {
                    stackTraceString += $"{sf.GetFileName()}:{sf.GetFileLineNumber()}{(i + 1 == stackTrace.FrameCount ? "" : "\n")}";
                }
            }
            return stackTraceString;
        }

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
            Scheduling.Job.Add(TimeFunctionWithStopwatchCoroutine(routine, numberOfExecutions));
        }

        static IEnumerator TimeFunctionWithStopwatchCoroutine(IEnumerator routine, int numberOfExecutions = 1)
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

        static void LogElapsedTime(int numberOfExecutions, double totalTime)
        {
            if (numberOfExecutions != 1)
            {
                LogManager.Log($"Average Execution Time: {(totalTime / numberOfExecutions).ToString("F10")} ms");
            }
            LogManager.Log($"Total Execution Time: {totalTime:F10} ms");
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}