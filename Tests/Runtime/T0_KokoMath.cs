/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 31, 2025
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
using Kokowolo.Utilities;

public class T0_KokoMath
{
    /*██████████████████████████████████████████████████████████*/
    #region Properties
    
    const string ScenePath = "Packages/com.kokowolo.utilities/Tests/Runtime/Scene.unity";
    
    #endregion
    /*██████████████████████████████████████████████████████████*/
    #region Functions
    /*——————————————————————————————————————————————————————————*/
    #region SetUp & TearDown

    [OneTimeSetUp] 
    public virtual void OneTimeSetUp()
    {
        TestUtils.LoadTestScene(ScenePath);
    }

    #endregion
    /*——————————————————————————————————————————————————————————*/
    #region Tests

    [Test]
    public void _00()
    {
        float value;
        value = KokoMath.WrapClamp(0, -10, 1);
        Debug.Assert(value == 0, value);
        value = KokoMath.WrapClamp(1, -10, 1);
        Debug.Assert(value == -10, value);
        value = KokoMath.WrapClamp(1, -10, 1);
        Debug.Assert(value == -10, value);
        value = KokoMath.WrapClamp(0.5f, 2.5f, 3.5f);
        Debug.Assert(value == 2.5f, value);
        
        // static float WrapClamp(float value, float minInclusive, float maxExclusive)
        // {
        //     float range = maxExclusive - minInclusive;
        //     return KokoMath.Mod(KokoMath.Mod(value - minInclusive, range) + range, range) + minInclusive;
        // }
    }

    #endregion
    /*——————————————————————————————————————————————————————————*/

    #endregion
    /*██████████████████████████████████████████████████████████*/
}