/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 19, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using Kokowolo.Utilities;

namespace Kokowolo.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneDrawer : PropertyDrawer
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        SceneAttribute Attribute => attribute as SceneAttribute;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                // currently only string is supported
                OnGUI_String(position, property, label);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use [Scene] with strings.");
            }
        }

        public void OnGUI_String(Rect position, SerializedProperty property, GUIContent label)
        {
            // get current scene
            SceneAsset prevScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);
            if (prevScene == null && !string.IsNullOrWhiteSpace(property.stringValue))
            {
                Debug.LogError($"Could not find scene {property.stringValue} in {property.propertyPath}");
            }

            // get new from ObjectField
            SceneAsset nextScene = (SceneAsset) EditorGUI.ObjectField(position, label, prevScene, typeof(SceneAsset), true);
            if (nextScene != null && Attribute.EnsureInBuildSettings)
            {
                bool found = false;
                foreach (EditorBuildSettingsScene buildScene in EditorBuildSettings.scenes)
                {
                    SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(buildScene.path);
                    if (sceneAsset == null || sceneAsset.name != nextScene.name) continue;
                    found = true;
                    break;
                }
                if (!found)
                {
                    Debug.LogWarning($"scene must be in ScenesInBuild.sceneData due to {nameof(Attribute.EnsureInBuildSettings)}");
                    return;
                }
            }

            // assign
            property.stringValue = AssetDatabase.GetAssetPath(nextScene);
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}