/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 23, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Kokowolo.Utilities.Tests
{
    public static class Utils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static void DestroyImmediateAll()
        {
            GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allGameObjects)
            {
                GameObject.DestroyImmediate(go);
            }
        }

        public static void EnsureTestSceneIsLoaded(string scenePath)
        {
            var scene1 = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(scenePath);
            if (scene1.isLoaded) return;
            LoadTestScene(scenePath);
        }

        public static void LoadTestScene(string scenePath)
        {
            EditorSceneManager.LoadSceneInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Single));
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}