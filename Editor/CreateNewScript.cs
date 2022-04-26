/*
 * File Name: CreateNewScript.cs
 * Description: This script is a helper script for creating new scripts within Kokowolo Projects
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 25, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    public class CreateNewScript
    {
        /************************************************************/
        #region Fields

        const string PathToScriptTemplates = "Packages/{0}/Editor/ScriptTemplates";

        const string cs = ".cs";
        const string txt = ".txt";

        #endregion
        /************************************************************/
        #region Functions

        public static void CreateScriptAssetFromName(string packageName, string name)
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(GetPathName(packageName, name),GetScriptFileName(name));
        }

        private static string GetPathName(string packageName, string name)
        {
            return $"{string.Format(PathToScriptTemplates, packageName)}/{name}{cs}{txt}";
        }

        private static string GetScriptFileName(string name)
        {
            return $"{name}{cs}";
        }

        #endregion
        /************************************************************/
    }
}