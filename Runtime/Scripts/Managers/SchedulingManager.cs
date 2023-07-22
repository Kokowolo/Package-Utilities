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

        #endregion
        /************************************************************/
        #region Functions

        public static void AddAsynchronousEvent(Action function, float time)
        {
            if (time <= 0) 
            {
                function.Invoke();
                return;
            }
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
        
        #endregion
        /************************************************************/
    }
}