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

                // write class pointer
                UnityEditor.EditorBuildSettingsScene firstBuildScene = null;
                foreach (UnityEditor.EditorBuildSettingsScene buildScene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!buildScene.enabled) continue;
                    firstBuildScene = buildScene;
                    break;
                }
                if (firstBuildScene == null)
                {
                    Debug.LogWarning($"No Scene in BuildSettings, but compiling {classFileName} anyway");
                }
                int firstSceneGUID = firstBuildScene == null? 0 : GetGuid(firstBuildScene);
                foreach (string line in GetClassPointerLines(firstSceneGUID))
                {
                    WriteLine(line);
                }
                WriteNewLine();

                // write class private data 1
                WriteLine(ClassPrivateDataLine1);
                UpdateIndentation(add: true);
                int sceneCount = 0;
                foreach (UnityEditor.EditorBuildSettingsScene buildScene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!buildScene.enabled) continue;
                    WriteLine($"{{ {sceneCount++}, {GetGuid(buildScene)} }},");
                }
                UpdateIndentation(add: false, writeLine: false);
                WriteLine("};");
                WriteNewLine();

                // write class private data 2
                WriteLine(ClassPrivateDataLine2);
                UpdateIndentation(add: true);
                sceneCount = 0;
                foreach (UnityEditor.EditorBuildSettingsScene buildScene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!buildScene.enabled) continue;
                    WriteLine($"{{ {GetGuid(buildScene)}, new SceneData({sceneCount++}, \"{buildScene.path}\") }},");
                }
                UpdateIndentation(add: false, writeLine: false);
                WriteLine("};");
                WriteNewLine();

                // write class fields
                WriteLine($"public static int Count => {sceneCount};");
                sceneCount = 0;
                foreach (UnityEditor.EditorBuildSettingsScene buildScene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (!buildScene.enabled) continue;
                    string sceneName = Path.GetFileNameWithoutExtension(buildScene.path).Replace(" ", "_");
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
                WriteNewLine();

                // write class functions
                foreach (string line in ClassFunctionLines)
                {
                    WriteLine(line);
                }
                // WriteNewLine();

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

        private int GetGuid(UnityEditor.EditorBuildSettingsScene buildScene)
        {
            return UnityEditor.AssetDatabase.GUIDFromAssetPath(buildScene.path).GetHashCode();
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
        #region Auto-Generated Script

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
            $"public class SceneData",
            $"{{",
            $"{tab}public SceneEnum sceneEnum => (SceneEnum) buildIndex;",
            $"{tab}public int buildIndex {{ get; }}",
            $"{tab}public string scenePath {{ get; }}",
            $"{tab}public string sceneName => System.IO.Path.GetFileNameWithoutExtension(scenePath);",
            $"{tab}public UnityEngine.SceneManagement.Scene Scene => UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(buildIndex);",
            $"{tab}internal SceneData(int buildIndex, string scenePath)",
            $"{tab}{{",
            $"{tab}{tab}this.buildIndex = buildIndex;",
            $"{tab}{tab}this.scenePath = scenePath;",
            $"{tab}}}",
            $"{tab}public override string ToString() => $\"{{buildIndex}}: {{scenePath}}\";",
            $"}}"
        };

        private string[] GetClassPointerLines(int guid) => new string[]
        {
            $"[System.Serializable]",
            $"public class SceneDataPointer",
            $"{{",
            $"{tab}[UnityEngine.SerializeField] internal int value = {guid};",
            $"{tab}public SceneData SceneData => sceneDataDict[value];",
            $"}}"
        };

        private string ClassPrivateDataLine1 => "private static System.Collections.Generic.Dictionary<int, int> sceneDataOrderingDict = new()";
        private string ClassPrivateDataLine2 => "private static System.Collections.Generic.Dictionary<int, SceneData> sceneDataDict = new()";

        private string[] GetClassFieldLines(string sceneName, int buildIndex) => new string[]
        {
            $"public static SceneData {sceneName} => GetSceneData({buildIndex});"
        };

        private string[] ClassFunctionLines => new string[]
        {
            $"internal static SceneData GetSceneData(int index) => sceneDataDict[sceneDataOrderingDict[index]];",
            $"internal static SceneDataPointer GetSceneDataPointer(SceneEnum sceneEnum) =>",
            $"{tab}new SceneDataPointer {{ value = sceneDataOrderingDict[(int) sceneEnum] }};",
        };

        private string[] ClassExtensionsLines => new string[]
        {
            $"public static class {className}Extensions",
            $"{{",
            $"{tab}public static {className}.SceneData ToSceneData(this {className}.SceneEnum sceneEnum)",
            $"{tab}{{",
            $"{tab}{tab}return {className}.GetSceneData((int) sceneEnum);",
            $"{tab}}}",
            $"}}"
        };

        private string[] ClassPropertyDrawerLines => new string[]
        {
            $"#if UNITY_EDITOR",
            $"",
            $"[UnityEditor.CustomPropertyDrawer(typeof({className}.SceneDataPointer))]",
            $"public class {className}Drawer : UnityEditor.PropertyDrawer",
            $"{{",
            $"{tab}public override void OnGUI(UnityEngine.Rect position, UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)",
            $"{tab}{{",
            $"{tab}{tab}if ({className}.Count == 0)",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}UnityEditor.EditorGUI.LabelField(position, label.text, \"No Scene in BuildSettings\");",
            $"{tab}{tab}{tab}return;",
            $"{tab}{tab}}}",
            $"{tab}{tab}{className}.SceneDataPointer pointer = ({className}.SceneDataPointer) property.boxedValue;",
            $"{tab}{tab}{className}.SceneEnum prevSceneEnum;",
            $"{tab}{tab}try",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}prevSceneEnum = ({className}.SceneEnum) pointer.SceneData.buildIndex;",
            $"{tab}{tab}}}",
            $"{tab}{tab}catch (System.Exception)",
            $"{tab}{tab}{{",
            $"{tab}{tab}{tab}UnityEngine.Debug.LogError(\"GUID not found in {className}'s dictionary, resetting {className}.SceneDataPointer\");",
            $"{tab}{tab}{tab}property.boxedValue = {className}.GetSceneDataPointer(0);",
            $"{tab}{tab}{tab}return;",
            $"{tab}{tab}}}",
            $"{tab}{tab}{className}.SceneEnum nextSceneEnum = ({className}.SceneEnum) UnityEditor.EditorGUI.EnumPopup(position, property.name, prevSceneEnum);",
            $"{tab}{tab}if (prevSceneEnum == nextSceneEnum) return;",
            $"{tab}{tab}property.boxedValue = {className}.GetSceneDataPointer(nextSceneEnum);",
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