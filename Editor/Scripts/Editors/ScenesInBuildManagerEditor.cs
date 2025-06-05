/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 17, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    [CustomEditor(typeof(ScenesInBuildManager))]
    public class ScenesInBuildManagerEditor : UnityEditor.Editor
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        private ScenesInBuildManager Target => target as ScenesInBuildManager;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Select Directory"))
            {
                var path = EditorUtility.OpenFolderPanel(
                    "Select Directory",
                    Target.ScriptDefaultDirectoryPath,
                    ""
                );
                Target.scriptDirectoryPath = path;
                EditorUtility.SetDirty(Target);
            }
            EditorGUILayout.Space();

            DrawDefaultInspector();

            EditorGUILayout.Space();
            if (GUILayout.Button(Target.EditorButtonText))
            {
                Target.RegenerateClass();
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}