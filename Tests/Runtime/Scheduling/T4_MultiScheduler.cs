/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 23, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */
 
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using TestUtils = Kokowolo.Utilities.Tests.Utils;

using Kokowolo.Utilities.Scheduling;

namespace Scheduling
{
    public class T4_MultiScheduler
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions
        /*——————————————————————————————————————————————————————————*/
        #region SetUp & TearDown

        [OneTimeSetUp] 
        public virtual void OneTimeSetUp()
        {
            TestUtils.DestroyImmediateAll();
            TestUtils.LoadTestScene(TestController.ScenePath);
        }

        [TearDown]
        public virtual void TearDown()
        {
            while (JobSystem.SchedulerCount > 0)
            {
                JobSystem.GetScheduler().Dispose();
            }
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Tests

        [UnityTest]
        public IEnumerator _00()
        {
            Debug.Assert(!JobSystem.IsInitialized);
            JobScheduler scheduler = JobScheduler.Create();
            Debug.Assert(JobSystem.GetScheduler() == scheduler);
            scheduler.Dispose();
            Debug.Assert(JobSystem.IsInitialized);
            Debug.Assert(JobSystem.SchedulerCount == 0);
            yield return null;
        }

        [UnityTest]
        public IEnumerator _01()
        {
            T2_Core t2_Core = new T2_Core();
            Job p0 = Job.Get(t2_Core._00());
            JobSystem.SetScheduler(JobScheduler.Create());
            Job p1 = Job.Get(t2_Core._01());
            yield return new WaitForJobScheduler();
            Debug.Assert(JobSystem.SchedulerCount == 2);
        }

        [UnityTest]
        public IEnumerator _02()
        {
            T2_Core t2_Core = new T2_Core();
            Job p0 = Job.Get(t2_Core._00());
            Job p1 = Job.Get(t2_Core._01());
            JobScheduler scheduler = JobSystem.GetScheduler();
            Debug.Assert(JobSystem.SchedulerCount == 1);
            scheduler.Dispose();
            yield return new WaitForJob(p0);
            yield return new WaitForJob(p1);
            yield return new WaitForJobScheduler(scheduler);
            Debug.Assert(JobSystem.SchedulerCount == 0);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}