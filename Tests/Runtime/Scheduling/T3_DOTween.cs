#if DOTWEEN
/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
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
using DG.Tweening;

using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

using TestUtils = Kokowolo.Utilities.Tests.Utils;

using Kokowolo.Utilities.Scheduling;

namespace Scheduling
{
    public class T3_DOTween
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields
        
        const float time = 0.1f;
        
        #endregion
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
            float xFinal = -2;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Tween t1 = cube.transform.DOMoveX(2, TestController.Time);
            yield return t1.WaitForCompletion();
            t1 = null;

            Tween t2 = cube.transform.DOMoveX(xFinal, TestController.Time);
            yield return t2.WaitForCompletion();
            t2 = null;

            Debug.Assert(cube.transform.position.x == xFinal);
            GameObject.DestroyImmediate(cube);
        }

        [UnityTest]
        public IEnumerator _01_0()
        {
            float xFinal = 2;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Job p0 = Job.WaitWhile(cube.transform.DOMoveX(xFinal, TestController.Time));
            yield return p0.WaitForCompletion();

            Debug.Assert(cube.transform.position.x == xFinal);
            GameObject.DestroyImmediate(cube);
        }

        [UnityTest]
        public IEnumerator _01_1()
        {
            float time = Mathf.Max(TestController.Time, 0);
            float xFinal = -2;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var sequence = DOTween.Sequence();
            sequence.Append(cube.transform.DOMoveX(2, time));
            Tween tween = cube.transform.DOMoveX(xFinal, time)
                .OnComplete(() => cube.transform.position = new Vector3(xFinal, cube.transform.position.y, cube.transform.position.z));
            sequence.Append(tween);
            Job p0 = Job.WaitWhile(sequence);
            yield return p0.WaitForCompletion();
            Debug.Assert(!tween.active);
            Debug.Assert(DOTween.TotalActiveTweens() == 0);

            Vector3 pos = cube.transform.position;

            Debug.Assert(cube.transform.position.x == xFinal);
            GameObject.DestroyImmediate(cube);
        }

        [UnityTest]
        public IEnumerator _02()
        {
            float time = Mathf.Max(TestController.Time, 0.1f);
            float xFinal = 2;
            TestController.Value = false;
            GameObject cube0 = GameObject.CreatePrimitive(PrimitiveType.Cube); cube0.transform.position = new(-1, -1);
            GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube); cube1.transform.position = new(-1, 1);
            GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube); cube2.transform.position = new(-1, 3);
            
            bool tweenCompleteFirst = false; // handles if time is too fast and tween completes on the same frame as job

            JobSequence s0 = JobSequence.Schedule();
            Job p1 = s0.Append(
                () => 
                {
                    TestController.Value = tweenCompleteFirst ? false : true; // roughly half way through tweens, set value
                    Debug.Log($"tweenCompleteFirst:{tweenCompleteFirst}");
                }, time / 2); 
            Tween tween = cube0.transform.DOMoveX(xFinal, time);
            tween.OnComplete(
                () => 
                {
                    TestController.Value = false; 
                    tweenCompleteFirst = true; // catches if the tween finishes too early
                    Debug.Log($"tweenCompleteFirst:{tweenCompleteFirst}"); 
                }); 
            // tween.OnComplete(() => TestController.Value = false); // fails to catch if the tween finishes too early
            s0.AppendWaitWhile(tween);
            s0.AppendWaitWhile(cube1.transform.DOMoveX(xFinal, time));
            s0.AppendWaitWhile(cube2.transform.DOMoveX(xFinal, time));

            yield return p1.WaitForCompletion();
            Debug.Assert(TestController.Value || tweenCompleteFirst);
            yield return s0.WaitForCompletion();
            Debug.Assert(!TestController.Value);

            Debug.Assert(cube0.transform.position.x == xFinal);
            Debug.Assert(cube1.transform.position.x == xFinal);
            Debug.Assert(cube2.transform.position.x == xFinal);

            GameObject.DestroyImmediate(cube0);
            GameObject.DestroyImmediate(cube1);
            GameObject.DestroyImmediate(cube2);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}
#endif