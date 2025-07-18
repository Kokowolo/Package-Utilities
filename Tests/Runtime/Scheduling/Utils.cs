/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 16, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Scheduling
{
    public static class Utils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        public const string TestScenePathName = "Packages/com.kokowolo.utilities/Tests/Runtime/Scheduling/Scene.unity";
        // public const string TestScenePathName = "Assets/Demos/Scheduling/Tests/Runtime/Scene.unity";

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static void EnsureTestSceneIsLoaded()
        {
            var scene1 = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(TestScenePathName);

            // scenes might be loaded, but destroy all GameObjects might have been called
            if (scene1.isLoaded) return;

            EditorSceneManager.LoadSceneInPlayMode(TestScenePathName, new LoadSceneParameters(LoadSceneMode.Single));
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}