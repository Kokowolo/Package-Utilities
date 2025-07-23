/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 15, 2025
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
    public class T1_LazyInit
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions
        /*——————————————————————————————————————————————————————————*/
        #region SetUp & TearDown

        [OneTimeSetUp] 
        public virtual void OneTimeSetUp()
        {
            TestUtils.DestroyImmediateAll();
        }

        [SetUp] 
        public virtual void SetUp()
        {
            TestUtils.LoadTestScene(TestController.ScenePath);
        }

        [TearDown]
        public virtual void TearDown()
        {
            TestUtils.DestroyImmediateAll();
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Tests

        [UnityTest]
        public IEnumerator _00()
        {
            Debug.Assert(TestController.Instance);
            Debug.Assert(!JobSystem.IsInitialized);
            Debug.Assert(JobSystem.IsFree);
            Debug.Assert(JobSystem.IsInitialized);
            yield return null; // Wait for instance to set... for some reason this cant run in a normal Test
        }

        [UnityTest]
        public IEnumerator _01()
        {
            Debug.Assert(TestController.Instance);
            Debug.Assert(!JobSystem.IsInitialized);
            Debug.Assert(JobScheduler.Main.IsFree);
            Debug.Assert(JobSystem.IsInitialized);
            yield return null; // Wait for instance to set... for some reason this cant run in a normal Test
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}