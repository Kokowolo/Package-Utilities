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
            TestUtils.LoadTestScene(TestController.ScenePath);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Tests

        [UnityTest]
        public IEnumerator _00()
        {
            JobScheduler scheduler = JobScheduler.Create();
            Debug.Assert(scheduler != JobScheduler.Main);
            scheduler.Dispose();
            Debug.Assert(JobSystem.SchedulersCount == 1);
            yield return null;
        }

        [UnityTest]
        public IEnumerator _01()
        {
            // JobScheduler scheduler = JobScheduler.Create();
            // Debug.Assert(scheduler != JobScheduler.Main);
            
            T2_Core t2_Core = new T2_Core();
            Job p0 = Job.Get(t2_Core._00());
            Job p1 = Job.Get(t2_Core._01());
            yield return new WaitForJobScheduler();

            // // Prepare GC check
            // WeakReference r0 = new WeakReference(p0);
            // WeakReference r1 = new WeakReference(p1);
            
            // // Demo check
            
            // Debug.Assert(p0.IsDisposed && p1.IsDisposed);

            // // Evaluate GC
            // p0 = p1 = p2 = null;
            // yield return null;
            // GC.Collect();
            // Debug.Assert(!r0.IsAlive && !r1.IsAlive);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}