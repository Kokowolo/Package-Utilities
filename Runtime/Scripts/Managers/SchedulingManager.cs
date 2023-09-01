/*
 * File Name: /*
 * File Name: SchedulingManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 12, 2023
 * 
 * Additional Comments:
 *      File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Kokowolo.Utilities;

namespace Kokowolo.Utilities
{
    public class SchedulingManager : MonoSingleton<SchedulingManager>
    {
        /************************************************************/
        #region Fields

        private Coroutine schedulerCoroutine;
        private List<IEnumerator> synchronousCoroutineQueue = new List<IEnumerator>();

        #endregion
        /************************************************************/
        #region Properties

        public static bool IsApplicationQuitting { get; private set; }

        #endregion
        /************************************************************/
        #region Functions

        protected override void MonoSingleton_Awake()
        {
            Application.quitting += Handle_Application_Quitting;
        }

        public static void AddAsynchronousEvent(Action function, float time)
        {
            time = Mathf.Max(time, 0);
            Instance.StartCoroutine(Instance.InvokeFunctionAfterTime(function, time));
        }

        public static void AddAsynchronousEvent(IEnumerator routine)
        {
            Instance.StartCoroutine(routine);
        }

        public static void AddSynchronousEvent(Action function, float time)
        {
            AddSynchronousEvent(Instance.InvokeFunctionAfterTime(function, time));
        }

        public static void AddSynchronousEvent(IEnumerator routine)
        {
            Instance.synchronousCoroutineQueue.Add(routine);
            if (Instance.schedulerCoroutine == null) 
            {
                Instance.schedulerCoroutine = Instance.StartCoroutine(Instance.InvokeSynchronousEvents());
            }
        }

        private IEnumerator InvokeFunctionAfterTime(Action function, float time)
        {
            yield return new WaitForSeconds(time);
            function.Invoke();
        }

        private IEnumerator InvokeSynchronousEvents()
        {
            while (synchronousCoroutineQueue.Count != 0)
            {
                yield return synchronousCoroutineQueue[0];
                synchronousCoroutineQueue.RemoveAt(0);
            }
            schedulerCoroutine = null;
        }

        private void Handle_Application_Quitting()
        {
            IsApplicationQuitting = true;
        }
        
        #endregion
        /************************************************************/
    }
}