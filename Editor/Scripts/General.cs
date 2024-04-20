/*
 * File Name: General.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 18, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.Linq;

namespace Kokowolo.Utilities.Editor
{
    public static class General
    {
        /************************************************************/
        #region Properties

        #endregion
        /************************************************************/
        #region Functions

        public static UnityEditor.PackageManager.PackageInfo GetPackageInfo(string packageName)
        {
            return AssetDatabase.FindAssets("package")
                .Select(AssetDatabase.GUIDToAssetPath)
                    .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(UnityEditor.PackageManager.PackageInfo.FindForAssetPath)
                    .Where(x => x != null)
                .First(x => x.name == packageName);
        }

        public static EditorBuildSettingsScene GetEditorBuildSettingsScene(int buildIndex, bool includeOnlyEnabled = true)
        {
            int index = -1;
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (includeOnlyEnabled && !scene.enabled) continue;
                index++;
                if (index == buildIndex) return scene;
            }
            return null;
        }
        
        public static int GetEditorBuildSettingsSceneBuildIndex(EditorBuildSettingsScene buildScene, bool includeOnlyEnabled = true)
        {
            int buildIndex = -1;
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (includeOnlyEnabled && !scene.enabled) continue;
                buildIndex++;
                if (scene.guid == buildScene.guid) return buildIndex;
            }
            return buildIndex;
        }

        public static int GetEditorBuildSettingsSceneBuildIndex(SceneAsset sceneAsset, bool includeOnlyEnabled = true)
        {
            int buildIndex = -1;
            foreach (EditorBuildSettingsScene buildScene in EditorBuildSettings.scenes)
            {
                if (includeOnlyEnabled && !buildScene.enabled) continue;
                buildIndex++;
                SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(buildScene.path);
                if (scene.GetInstanceID() == sceneAsset.GetInstanceID()) return buildIndex;
            }
            return buildIndex;
        }

        #endregion
        /************************************************************/
    }
}