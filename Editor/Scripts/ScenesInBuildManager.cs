/*
 * File Name: ScenesInBuildManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 17, 2024
 * 
 * Additional Comments:
 *      File Line Length: 140
 */

 #if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.SceneManagement;

namespace Kokowolo.Utilities
{
    [CreateAssetMenu(menuName = "Kokowolo/Utilities/ScenesInBuildManager Asset", fileName = nameof(ScenesInBuildManager))]
    public sealed class ScenesInBuildManager : ScriptableObject
    {
        /************************************************************/
        #region Fields

        [Header("Class Settings")]
        [SerializeField, ReadOnly] public string scriptDirectoryPath = "";
        [SerializeField] public string classFileName = "ScenesInBuild_AutoGenerated";
        [SerializeField] public string className = "ScenesInBuild";
        [SerializeField] string classNamespace;

        [Header("Additional Settings")]
        [Tooltip("exact string of the whitespace indentation to use within the auto-generated file")]
        [SerializeField] string whitespaceIndentation = "    "; // default is 4 whitespace characters
        // [Tooltip("char to replace whitespace, ` `, in any scene name")]
        // [SerializeField] string whitespaceReplaceChar = "_"; // default is 4 whitespace characters

        private bool isGenerating = false;
        private string currentWhitespaceIndentation = "";

        StreamWriter file;

        #endregion
        /************************************************************/
        #region Properties

        public string EditorButtonText => isGenerating ? "Regenerating..." : "Regenerate Class";

        public string ScriptDefaultDirectoryPath => Path.GetDirectoryName(UnityEditor.AssetDatabase.GetAssetPath(this));
        private string ScriptDirectoryPath => scriptDirectoryPath.Length == 0 ? ScriptDefaultDirectoryPath : scriptDirectoryPath;
        private string ScriptFilePath => Path.Combine(ScriptDirectoryPath, $"{classFileName}.cs");

        bool HasClassNamespace => classNamespace.Length > 0;

        string tab => whitespaceIndentation; // convenience property

        #endregion
        /************************************************************/
        #region Functions

        // public void OnValidate()
        // {
        //     whitespaceReplaceChar = whitespaceReplaceChar.Length > 1 ? whitespaceReplaceChar[..1] : whitespaceReplaceChar;
        //     whitespaceReplaceChar = whitespaceReplaceChar.Equals(" ") ? "_" : whitespaceReplaceChar;
        // }

        public void RegenerateClass()
        {
            // start
            isGenerating = true;
            currentWhitespaceIndentation = "";
            Debug.Log("Writing File");

            using (file = new StreamWriter(ScriptFilePath))
            {
                // write comments
                foreach (string line in CommentLines)
                {
                    WriteLine(line);
                }
                WriteNewLine();

                // write namespace
                if (HasClassNamespace)
                {
                    WriteLine(ClassNamespaceLine);
                    UpdateIndentation(add: true);
                }

                // write class declaration
                WriteLine(ClassDeclarationLine);
                UpdateIndentation(add: true);

                // get scene names and ensure no duplicates
                Dictionary<string, int> sceneNamesDict = new Dictionary<string, int>();
                sceneNamesDict.Add($"{className}", 0); // NOTE: ensures no scene can be named after `className`
                foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!scene.enabled) continue;

                    string sceneName = Path.GetFileNameWithoutExtension(scene.path).Replace(" ", "_");
                    // NOTE: this is done so that only duplicates are named
                    sceneNamesDict[sceneName] = sceneNamesDict.ContainsKey(sceneName) ? 0 : -1;
                }

                // write class Enum declaration
                WriteLine(ClassEnumDeclarationLine);
                UpdateIndentation(add: true);
                foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!scene.enabled) continue;
                    string sceneName = Path.GetFileNameWithoutExtension(scene.path).Replace(" ", "_");
                    if (sceneNamesDict[sceneName] != -1)
                    {
                        sceneName = $"{sceneName}_{sceneNamesDict[sceneName]++}";
                    }
                    WriteLine($"{sceneName},");
                }
                UpdateIndentation(add: false);
                WriteNewLine();

                // reset dictionary
                var keys = new List<string>(sceneNamesDict.Keys);
                foreach (var key in keys)
                {
                    sceneNamesDict[key] = sceneNamesDict[key] > -1 ? 0 : -1;
                }
                sceneNamesDict[className] = 0;

                // write class subclass
                foreach (string line in ClassSubclassLines)
                {
                    WriteLine(line);
                }
                WriteNewLine();

                // write class private data
                WriteLine(ClassPrivateDataLine);
                UpdateIndentation(add: true);
                int sceneCount = 0;
                foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!scene.enabled) continue;
                    WriteLine($"new SceneData({sceneCount++}, \"{scene.path}\"),");
                }
                UpdateIndentation(add: false, writeLine: false);
                WriteLine("};");
                WriteNewLine();

                // write Scenes class's fields
                sceneCount = 0;
                foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!scene.enabled) continue;
                    string sceneName = Path.GetFileNameWithoutExtension(scene.path).Replace(" ", "_");
                    if (sceneNamesDict[sceneName] != -1)
                    {
                        sceneName = $"{sceneName}_{sceneNamesDict[sceneName]++}";
                    }

                    foreach (string line in GetClassFieldLines(sceneName, sceneCount))
                    {
                        WriteLine(line);
                    }
                    sceneCount++;
                }

                // close class
                UpdateIndentation(add: false);
                WriteNewLine();

                // write Extensions class
                foreach (string line in ClassExtensionsLines)
                {
                    WriteLine(line);
                }
                WriteNewLine();

                // write class PropertyDrawer
                foreach (string line in ClassPropertyDrawerLines)
                {
                    WriteLine(line);
                }

                // close
                while (currentWhitespaceIndentation.Length > 0)
                {
                    UpdateIndentation(add: false);
                }
            }

            // done!
            Debug.Log("Closing File");
            UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
            isGenerating = false;
        }

        private void WriteLine(string line)
        {
            if (line.Length > 0 && line[0] == '#')
            {
                file.WriteLine(line); // don't indent compile directives
            }
            else
            {
                file.WriteLine($"{currentWhitespaceIndentation}{line}");
            }
        }

        private void WriteNewLine()
        {
            WriteLine("");
        }

        private void UpdateIndentation(bool add, bool writeLine = true)
        {
            if (add)
            {
                if (writeLine) WriteLine("{");
                currentWhitespaceIndentation += tab;
            }
            else
            {
                currentWhitespaceIndentation = currentWhitespaceIndentation.Remove(0, tab.Length);
                if (writeLine) WriteLine("}");
            }
        } 

        #endregion
        /************************************************************/
        #region Auto-Generated Script Text

        private string packageName = "com.kokowolo.utilities";

        private string[] CommentLines => new string[]
        { 
            $"//------------------------------------------------------------------------------", 
            $"// <auto-generated>", 
            $"//     This code was auto-generated by {packageName}:{nameof(ScenesInBuildManager)}", 
            $"//     version {Kokowolo.Utilities.Editor.General.GetPackageInfo(packageName).version}", 
            $"//     from {UnityEditor.AssetDatabase.GetAssetPath(this)}",
            $"//",
            $"//     Changes to this file may cause incorrect behavior and will be lost if",
            $"//     the code is regenerated.",
            $"// </auto-generated>",
            $"//------------------------------------------------------------------------------"
        };

        private string ClassNamespaceLine => $"namespace {classNamespace}";

        private string ClassDeclarationLine => $"public static class {className}";

        private string ClassEnumDeclarationLine => "public enum SceneEnum";

        private string[] ClassSubclassLines => new string[]
        {
            $"[System.Serializable]",
            $"public class SceneData",
            $"{{",
            $"#if UNITY_EDITOR",
            $"{tab}[UnityEngine.SerializeField] public UnityEditor.SceneAsset sceneAsset;",
            $"#endif",
            $"{tab}[UnityEngine.SerializeField] int _buildIndex = -1;",
            $"{tab}public int buildIndex => _buildIndex;",
            $"{tab}[UnityEngine.SerializeField] string _scenePath;",
            $"{tab}public string scenePath => _scenePath;",
            $"{tab}public string sceneName => System.IO.Path.GetFileNameWithoutExtension(scenePath);",
            $"{tab}public SceneData(int buildIndex, string scenePath)",
            $"{tab}{{",
            $"{tab}{tab}this._buildIndex = buildIndex;",
            $"{tab}{tab}this._scenePath = scenePath;",
            $"#if UNITY_EDITOR",
            $"{tab}this.sceneAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.SceneAsset>(_scenePath);",
            $"#endif",
            $"{tab}}}",
            $"{tab}public UnityEngine.SceneManagement.Scene GetScene() => UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(buildIndex);",
            $"{tab}public override string ToString() => $\"{{buildIndex}}: {{scenePath}}\";",
            $"}}"
        };

        private string ClassPrivateDataLine => "internal static SceneData[] sceneData = new SceneData[]";

        private string[] GetClassFieldLines(string sceneName, int buildIndex)
        {
            return new string[]
            {
                $"public static SceneData {sceneName} => sceneData[{buildIndex}];"
                // $"public static UnityEngine.SceneManagement.Scene {sceneName} = ",
                // $"{whitespaceIndentation}UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex({buildIndex});"
            };
        }

        private string[] ClassExtensionsLines => new string[]
        {
            $"public static class {className}Extensions",
            $"{{",
            $"{tab}public static {className}.SceneData ToSceneData(this {className}.SceneEnum sceneEnum)",
            $"{tab}{{",
            $"{tab}{tab}return {className}.sceneData[(int) sceneEnum];",
            $"{tab}}}",
            $"}}"
        };

        private string[] ClassPropertyDrawerLines => new string[]
        {
            $"#if UNITY_EDITOR",
            $"",
            $"[UnityEditor.CustomPropertyDrawer(typeof({className}.SceneData))]",
            $"public class {className}Drawer : UnityEditor.PropertyDrawer",
            $"{{",
            $"{tab}public override void OnGUI(UnityEngine.Rect position, UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)",
            $"{tab}{{",
            $"{tab}{tab}if ({className}.sceneData.Length == 0)",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}UnityEditor.EditorGUI.LabelField(position, label.text, \"No Scene in BuildSettings\");",
            $"{tab}{tab}{tab}return;",
            $"{tab}{tab}}}",
            $"{tab}{tab}bool reserialize = false;",
            $"{tab}{tab}{className}.SceneEnum prevSceneEnum;",
            $"{tab}{tab}{className}.SceneData sceneData = property.boxedValue as {className}.SceneData;",
            $"{tab}{tab}if (sceneData.sceneAsset == null)",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}sceneData = GetSceneData(({className}.SceneEnum) sceneData.buildIndex);",
            $"{tab}{tab}{tab}reserialize = true;",
            $"{tab}{tab}}}",
            $"{tab}{tab}else",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}UnityEditor.SceneAsset sceneAsset = sceneData.sceneAsset;",
            $"{tab}{tab}{tab}int buildIndex = Kokowolo.Utilities.Editor.General.GetEditorBuildSettingsSceneBuildIndex(sceneAsset);",
            $"{tab}{tab}{tab}prevSceneEnum = ({className}.SceneEnum) buildIndex;",
            $"{tab}{tab}{tab}if (buildIndex != sceneData.buildIndex || UnityEditor.AssetDatabase.GetAssetPath(sceneAsset) != sceneData.scenePath)",
            $"{tab}{tab}{tab}{{",
            $"{tab}{tab}{tab}{tab}if (!System.Enum.IsDefined(typeof({className}.SceneEnum), prevSceneEnum))",
            $"{tab}{tab}{tab}{tab}{{",
            $"{tab}{tab}{tab}{tab}{tab}UnityEngine.Debug.LogError($\"scene {{sceneAsset.name}} is no longer found within build settings\");",
            $"{tab}{tab}{tab}{tab}{tab}prevSceneEnum = 0;",
            $"{tab}{tab}{tab}{tab}}}",
            $"{tab}{tab}{tab}{tab}sceneData = GetSceneData(prevSceneEnum);",
            $"{tab}{tab}{tab}{tab}reserialize = true;",
            $"{tab}{tab}{tab}{tab}if (sceneAsset != sceneData.sceneAsset || UnityEditor.AssetDatabase.GetAssetPath(sceneData.sceneAsset) != sceneData.scenePath)",
            $"{tab}{tab}{tab}{tab}{{",
            $"{tab}{tab}{tab}{tab}{tab}UnityEditor.EditorGUI.LabelField(position, label.text, \"{className} needs regeneration\");",
            $"{tab}{tab}{tab}{tab}{tab}return;",
            $"{tab}{tab}{tab}{tab}}}",
            $"{tab}{tab}{tab}}}",
            $"{tab}{tab}}}",
            $"{tab}{tab}prevSceneEnum = ({className}.SceneEnum) sceneData.buildIndex;",
            $"{tab}{tab}{className}.SceneEnum nextSceneEnum = ({className}.SceneEnum) UnityEditor.EditorGUI.EnumPopup(position, property.name, prevSceneEnum);",
            $"{tab}{tab}if (prevSceneEnum != nextSceneEnum)",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}sceneData = GetSceneData(nextSceneEnum);",
            $"{tab}{tab}{tab}reserialize = true;",
            $"{tab}{tab}}}",
            $"{tab}{tab}if (reserialize) property.boxedValue = sceneData;",
            $"{tab}}}",
            $"",
            $"{tab}private {className}.SceneData GetSceneData({className}.SceneEnum sceneEnum)",
            $"{tab}{{",
            $"{tab}{tab}foreach ({className}.SceneData data in {className}.sceneData)",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}if (data.buildIndex == (int) sceneEnum) return data;",
            $"{tab}{tab}}}",
            $"{tab}{tab}throw new System.Exception();",
            $"{tab}}}",
            $"}}",
            $"",
            $"#endif",
        };

        #endregion
        /************************************************************/
    }

}
#endif