/*
 * File Name: LogManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: November 14, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

// #define USE_LOGGER 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Globalization;

namespace Kokowolo.Utilities
{
    // [DefaultExecutionOrder(-2000)]
    public class LogManager// : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        // [SerializeField] bool showClass = true;

        //private static GameLogHandler logHandler;

        #endregion
        /************************************************************/
        #region Properties

        #endregion
        /************************************************************/
        #region Functions

        // private void Awake() 
        // {
        //     if (logHandler != null) return;


        //     // Replace the default debug log handler
        //     logHandler = new GameLogHandler(showClass); 
        //     Debug.unityLogger.logHandler = logHandler;
        // }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(object message, UnityEngine.Object context = null)
        {
            Debug.Log(GetString(message), context);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogWarning(object message, UnityEngine.Object context = null)
        {
            Debug.LogWarning(GetString(message), context);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogError(object message, UnityEngine.Object context = null)
        {
            Debug.LogError(GetString(message), context);
        }

        private static string GetString(object message)
        {
            if (message == null) return $"[{GetCallingClassName(3)}] Null";

            var formattable = message as IFormattable;
            if (formattable != null)
            {
                return $"[{GetCallingClassName(3)}] {formattable.ToString(null, CultureInfo.InvariantCulture)}";
            }
            else
            {
                return $"[{GetCallingClassName(3)}] {message.ToString()}";
            }
        }

        private static string GetCallingClassName(int depth)
        {   
            return new System.Diagnostics.StackTrace().GetFrame(depth).GetMethod().ReflectedType.Name;
        }

        #endregion
        /************************************************************/
    }
}