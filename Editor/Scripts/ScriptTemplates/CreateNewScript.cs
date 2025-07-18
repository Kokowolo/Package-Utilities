/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 25, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using UnityEditor;
using System.IO;

namespace Kokowolo.Utilities.Editor
{
    /// <summary>
    /// A helper class for creating new scripts within Kokowolo Projects
    /// </summary>
    public static class CreateNewScript
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static void CreateFileFromScriptTemplatesFileName(string scriptTemplatesFileName)
        {
            string templateFilePath = Path.Combine(
                Directory.GetCurrentDirectory(), 
                $"Assets/Editor/ScriptTemplates/{scriptTemplatesFileName}"
            );
            CreateFileFromTemplateFilePath(templateFilePath);
        }

        public static void CreateFileFromTemplateFilePath(string templateFilePath)
        {
            string templateFileName = System.IO.Path.GetFileName(templateFilePath);
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                templateFilePath, 
                RemoveTxtExtension(templateFileName)
            );
        }

        /// <summary>
        /// creates a file from a template file located at Packages/{packageName}/Editor/ScriptTemplates
        /// </summary>
        /// <param name="packageName">name of the package calling this script, i.e. "com.kokowolo.utilities"</param>
        /// <param name="scriptTemplatesFileName">script template file name (with all of its extensions)</param>
        public static void CreateFileFromPackageTemplateFile(string packageName, string scriptTemplatesFileName)
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                $"{string.Format("Packages/{0}/Editor/ScriptTemplates", packageName)}/{scriptTemplatesFileName}", 
                RemoveTxtExtension(scriptTemplatesFileName)
            );
        }
        
        private static string RemoveTxtExtension(string fileName)
        {
            return fileName.Substring(0, fileName.Length - 4);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}