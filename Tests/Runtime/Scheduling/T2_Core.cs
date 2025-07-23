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
    public class T2_Core
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
            // Demo main
            int value = 0;
            Job p0 = Job.Get(Function1, TestController.Time);
            Job p1 = Job.Get(Function1, TestController.Time);
            Job p2 = Job.Get(Function1, TestController.Time);

            // Declare local function
            void Function1()
            {
                value += 1;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(p0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            Debug.Assert(value == 0);
            yield return new WaitForJob(p0);
            yield return new WaitForJob(p1);
            yield return new WaitForJob(p2);
            Debug.Assert(value == 3);
            Debug.Assert(p0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            p0 = p1 = p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _01()
        {
            // Demo main
            int value = 0;
            int increment = 1;
            Job p0 = Job.Get(Function1(increment++));
            Job p1 = Job.Get(Function1(increment++));
            Job p2 = Job.Get(Function1(increment++));

            // Declare local function
            IEnumerator Function1(int i)
            {
                yield return new WaitForSeconds(TestController.Time);
                value += i;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(p0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            Debug.Assert(value == 0, $"v1:{value}");
            yield return new WaitForJob(p0);
            yield return new WaitForJob(p1);
            yield return new WaitForJob(p2);
            Debug.Assert(value == 6, $"v1:{value}");
            Debug.Assert(p0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            p0 = p1 = p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _02_0()
        {
            // Demo main
            int value = 0;
            Job p0 = Job.Schedule(Function1, TestController.Time);
            Job p1 = Job.Schedule(Function1, TestController.Time);
            Job p2 = Job.Schedule(Function1, TestController.Time);

            // Declare local function
            void Function1()
            {
                value += 1;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(p0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            Debug.Assert(value == 0);
            p1.OnComplete(() => Debug.Assert(value == 2));
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 3);
            Debug.Assert(p0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            p0 = p1 = p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _02_1()
        {
            // Demo main
            int value = 0;
            int increment = 1;
            Job p0 = Job.Schedule(Function1(increment++));
            Job p1 = Job.Schedule(Function1(increment++));
            Job p2 = Job.Schedule(Function1(increment++));

            // Declare local function
            IEnumerator Function1(int i)
            {
                yield return new WaitForSeconds(TestController.Time);
                value += i;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(p0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            Debug.Assert(value == 0);
            yield return new WaitForJob(p1);
            Debug.Assert(value == 3, $"value:{value}");
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 6, $"value:{value}");
            Debug.Assert(p0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            p0 = p1 = p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _03_0()
        {
            // Demo main
            int value = 0;
            JobSequence s0 = JobSequence.Get();
            Job p1 = s0.Append(Function1, TestController.Time);
            s0.Append(Function1, TestController.Time);
            Job p2 = s0.Append(Function1, TestController.Time);

            // Declare local function
            void Function1()
            {
                value += 1;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(s0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            Debug.Assert(value == 0);
            p1.OnComplete(() => Debug.Assert(value == 1));
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 3);
            yield return new WaitForJob(p2);
            Debug.Assert(value == 3);
            yield return new WaitForJob(s0);
            Debug.Assert(value == 3);
            Debug.Assert(s0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            s0 = null;
            p1 = null;
            p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _03_1()
        {
            // Demo main
            int value = 0;
            JobSequence s0 = JobSequence.Get();
            Job p1 = s0.Append(Function1(1, TestController.Time * 3));
            Job p2 = s0.Append(Function1(3, TestController.Time));
            s0.Append(Function1(2, TestController.Time));
            
            // Declare local function
            IEnumerator Function1(int i, float time)
            {
                yield return new WaitForSeconds(time);
                value += i;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(s0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            Debug.Assert(value == 0);
            p1.OnComplete(() => Debug.Assert(value == 1));
            p2.OnComplete(() => Debug.Assert(value == 4));
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 6);
            yield return new WaitForJob(s0);
            Debug.Assert(value == 6);
            Debug.Assert(s0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            s0 = null;
            p1 = null;
            p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _03_2()
        {
            // Demo main
            int value = 0;
            JobSequence s0 = JobSequence.Get();
            s0.Append(Function1, .3f);
            Job p1 = s0.Append(Function1);
            s0.Append(Function1);
            Job p2 = Job.Get(Function1);

            // Declare local function
            void Function1()
            {
                value += 1;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(s0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            Debug.Assert(value == 0);
            p2.OnComplete(() => Debug.Assert(value == 1));
            yield return new WaitForJob(s0);
            Debug.Assert(value == 4);
            Debug.Assert(JobScheduler.Main.IsFree);
            Debug.Assert(s0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            s0 = null;
            p1 = null;
            p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _04()
        {
            // Demo main
            float time = -1;
            int value = 0; // v_f = 2 + 5 * 2
            Job p0 = Job.Get(Add(5))
                .OnComplete(
                    () => Job.Get(Mult(2))
                        .OnComplete(
                            () => Job.Get(Add(2))
                        )
                );

            // Declare local function
            IEnumerator Add(int number)
            {
                if (time >= 0) yield return new WaitForSeconds(time);
                value += number;
            }
            IEnumerator Mult(int number) 
            {
                if (time >= 0) yield return new WaitForSeconds(time);
                value *= number;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(p0);
            Debug.Assert(r0.IsAlive);

            // Demo check
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 12);
            Debug.Assert(JobScheduler.Main.IsFree);
            Debug.Assert(p0.IsDisposed);

            // Evaluate GC
            p0 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive);
        }

        [UnityTest]
        public IEnumerator _05()
        {
            // Demo main
            int value = 0;
            Job p1 = Job.Schedule(Add(1, .1f));
            Job p2 = Job.Schedule(Add(1, .1f));
            JobSequence s0 = JobSequence.Schedule();
            s0.Append(Add(3, -1));
            s0.Append(Mult(2, -1));
            s0.Append(Add(2, -1));
            Job p4 = Job.Schedule(Add(1000, .1f));

            // Declare local function
            IEnumerator Add(int number, float time)
            {
                if (time >= 0) yield return new WaitForSeconds(time);
                value += number;
            }
            IEnumerator Mult(int number, float time) 
            {
                if (time >= 0) yield return new WaitForSeconds(time);
                value *= number;
            }

            // Prepare GC check
            WeakReference r0 = new WeakReference(s0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            WeakReference r4 = new WeakReference(p4);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive && r4.IsAlive);

            // Demo check
            yield return new WaitForJob(s0);
            Debug.Assert(value == 12);
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 1012);
            Debug.Assert(JobScheduler.Main.IsFree);
            Debug.Assert(s0.IsDisposed && p1.IsDisposed && p2.IsDisposed && p4.IsDisposed);

            // Evaluate GC
            s0 = null;
            p1 = null;
            p2 = null;
            p4 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive && !r4.IsAlive);
        }

        [UnityTest]
        public IEnumerator _06()
        {
            // Demo main
            int value = 0;
            JobSequence s0 = JobSequence.Schedule();
            s0.Append(() => Add(1));
            var p1 = s0.Append(() => Add(2));
            var p2 = s0.Append(() => Add(3));
            s0.Append(() => Add(9));

            p1.OnComplete(() => p2.Dispose(false));

            // Declare local function
            void Add(int number) => value += number;

            // Prepare GC check
            WeakReference r0 = new WeakReference(s0);
            WeakReference r1 = new WeakReference(p1);
            WeakReference r2 = new WeakReference(p2);
            Debug.Assert(r0.IsAlive && r1.IsAlive && r2.IsAlive);

            // Demo check
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 12, $"value:{value}");
            Debug.Assert(s0.IsDisposed && p1.IsDisposed && p2.IsDisposed);

            // Evaluate GC
            s0 = null;
            p1 = null;
            p2 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r2.IsAlive);
        }

        [UnityTest]
        public IEnumerator _07()
        {
            // Demo main
            int value = 0;
            JobSequence s0 = JobSequence.Schedule();
            var p1 = s0.Append(() => Add(1));
            s0.Append(() => Add(2));

            Job p3 = null;
            WeakReference r3 = null;
            p1.OnStart(() =>
            {
                Debug.Assert(value == 0);
                p3 = s0.Append(() => Add(100));
                r3 = new WeakReference(p3);
                Debug.Assert(!r3.IsAlive);

                // New API, now only the first assertion should run
                Debug.Assert(p3 == null);
                p3?.Dispose();
                p3?.OnStart(() => Debug.Assert(false));
            });

            // Declare local function
            void Add(int number) => value += number;

            // Prepare GC check
            WeakReference r0 = new WeakReference(s0);
            WeakReference r1 = new WeakReference(p1);
            Debug.Assert(r0.IsAlive && r1.IsAlive);

            // Demo check
            yield return new WaitForJobScheduler();
            Debug.Assert(value == 3, $"value:{value}");
            Debug.Assert(s0.IsDisposed && p1.IsDisposed && p3 == null);

            // Evaluate GC
            s0 = null;
            p1 = null;
            p3 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive && !r3.IsAlive);
        }

        [UnityTest]
        public IEnumerator _08_0()
        {
            TestController.Value = true;
            Job p0 = Job.WaitWhile(Function);

            // Declare local function
            bool Function() => TestController.Value;

            // Prepare GC check
            WeakReference r0 = new WeakReference(p0);
            Debug.Assert(r0.IsAlive);
            
            // Demo check
            Debug.Assert(!p0.IsDisposed);

            if (TestController.UseUserInput)
            {
                yield return p0.WaitForCompletion();
            }
            else
            {
                if (TestController.Time > 0)
                {
                    yield return new WaitForSeconds(TestController.Time);
                    Debug.Assert(!p0.IsDisposed);
                }
                TestController.Value = false;
                yield return null;
                yield return null;
            }
            Debug.Assert(p0.IsDisposed);

            // Evaluate GC
            p0 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive);
        }

        [UnityTest]
        public IEnumerator _08_1()
        {
            TestController.Value = true;
            Job p0 = Job.ScheduleWaitWhile(Function);
            Job p1 = Job.ScheduleWaitWhile(Function);
            p0.OnComplete(() => TestController.Value = true);

            // Declare local function
            bool Function() => TestController.Value;

            // Prepare GC check
            WeakReference r0 = new WeakReference(p0);
            WeakReference r1 = new WeakReference(p1);
            Debug.Assert(r0.IsAlive && r1.IsAlive);
            
            // Demo check
            Debug.Assert(!p0.IsDisposed && !p1.IsDisposed);

            if (TestController.UseUserInput)
            {
                yield return p0.WaitForCompletion();
                Debug.Assert(p0.IsDisposed);
                yield return p1.WaitForCompletion();
                Debug.Assert(p1.IsDisposed);
            }
            else
            {
                // First wait
                if (TestController.Time > 0)
                {
                    yield return new WaitForSeconds(TestController.Time);
                    Debug.Assert(!p0.IsDisposed);
                }
                TestController.Value = false;
                yield return null;
                yield return null;
                Debug.Assert(p0.IsDisposed);

                // second wait
                if (TestController.Time > 0)
                {
                    yield return new WaitForSeconds(TestController.Time);
                    Debug.Assert(!p1.IsDisposed);
                }
                TestController.Value = false;
                yield return null;
                yield return null;
                Debug.Assert(p1.IsDisposed);
            }

            // Evaluate GC
            p0 = null;
            p1 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive && !r1.IsAlive);
        }

        [UnityTest]
        public IEnumerator _08_2()
        {
            TestController.Value = false;
            JobSequence s0 = JobSequence.Get();
            Job p1 = s0.Append(Function2);
            s0.AppendWaitWhile(Function1);
            s0.Append(Function2);

            // Declare local function
            bool Function1() => TestController.Value;
            void Function2() => TestController.Value = true;

            // Prepare GC check
            WeakReference r0 = new WeakReference(s0);
            WeakReference r1 = new WeakReference(p1);
            Debug.Assert(r0.IsAlive && r1.IsAlive);
            
            // Demo check
            Debug.Assert(!s0.IsDisposed);
            if (TestController.UseUserInput)
            {
                yield return s0.WaitForCompletion();
                Debug.Assert(s0.IsDisposed);
            }
            else
            {
                yield return p1.WaitForCompletion();
                TestController.Value = false;
                yield return s0.WaitForCompletion();
                Debug.Assert(s0.IsDisposed);
            }

            Debug.Assert(TestController.Value);

            // Evaluate GC
            s0 = null;
            yield return null;
            GC.Collect();
            Debug.Assert(!r0.IsAlive);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}