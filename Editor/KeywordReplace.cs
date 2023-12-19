/*
 * File Name: CreateNewMonoBehaviour.cs
 * Description: This script is for creating new MonoBehaviour Scripts within Kokowolo Projects; for more info, see 
 *              CreateNewScript.cs
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 26, 2022
 * 
 * Additional Comments:
 *		While this file has been updated to better fit this project, the original version can be found here:
 *		https://forum.unity.com/threads/c-script-template-how-to-make-custom-changes.273191/ thanks to hpjohn
 *
 *		File Line Length: 120
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace Kokowolo.Utilities.Editor
{
    public class KeywordReplace : UnityEditor.AssetModificationProcessor
    {
        /************************************************************/
        #region Functions

        public static void OnWillCreateAsset(string path)
        {
            if (!path.EndsWith(".cs.meta"))
            {
                return;
            }
            path = path.Replace(".meta", "");
            int index = path.LastIndexOf(".");
            string file = path.Substring(index);

            if (file != ".cs" && file != ".js" && file != ".boo") return;

            index = Application.dataPath.LastIndexOf("Assets");
            path = Application.dataPath.Substring(0, index) + path;
            file = System.IO.File.ReadAllText(path);

            string date = $"{DateTime.Now.ToString("MMMM")} {DateTime.Now.Day}, {DateTime.Now.Year}";

            file = file.Replace("#DATE#", date);
            file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);
            file = file.Replace("#PRODUCTNAME#", PlayerSettings.productName);

            System.IO.File.WriteAllText(path, file);
            AssetDatabase.Refresh();
        }
        #endregion
        /************************************************************/
    }
}