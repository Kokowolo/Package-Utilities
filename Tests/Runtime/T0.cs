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

using Kokowolo.Utilities.Tests;
using Kokowolo.Utilities;

public class RandomizerTest
{
    /*██████████████████████████████████████████████████████████*/
    #region Properties

    public const string ScenePath = "Packages/com.kokowolo.utilities/Tests/Runtime/Scene.unity";

    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Functions
    /*——————————————————————————————————————————————————————————*/
    #region SetUp & TearDown

    [OneTimeSetUp] 
    public virtual void OneTimeSetUp()
    {
        Utils.LoadTestScene(ScenePath);
    }

    #endregion
    /*——————————————————————————————————————————————————————————*/
    #region Tests

    [UnityTest]
    public IEnumerator _0()
    {
        yield return null;
        var scene1 = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(ScenePath);
        Debug.Assert(scene1.isLoaded);
    }

    [UnityTest]
    public IEnumerator _1()
    {
        // Init
        Randomizer s0 = new Randomizer(0);
        Randomizer s1 = new Randomizer(-1);
        WeakReference r0 = new WeakReference(s0);
        WeakReference r1 = new WeakReference(s1);
        // Test
        Debug.Assert(s0.Seed != 0 && s1.Seed != -1);
        Debug.Assert(r0.IsAlive && r1.IsAlive);
        // Close
        s0 = null;
        s1 = null;
        yield return null;
        GC.Collect();
        Debug.Assert(!r0.IsAlive && !r1.IsAlive);
    }

    #endregion
    /*——————————————————————————————————————————————————————————*/

    #endregion
    /*██████████████████████████████████████████████████████████*/
}