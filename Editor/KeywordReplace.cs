using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class KeywordReplace : UnityEditor.AssetModificationProcessor
{
    /************************************************************/
    #region Properties

    private static string Date => $"{DateTime.Now.ToString("MMMM")} {DateTime.Now.Day}, {DateTime.Now.Year}";

    #endregion
    /************************************************************/
    #region Functions

    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string file = path.Substring(index);

        if (file != ".cs" && file != ".js" && file != ".boo") return;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        file = file.Replace("#DATE#", Date);
        file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);
        file = file.Replace("#PRODUCTNAME#", PlayerSettings.productName);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
    #endregion
    /************************************************************/

#if UNITY_EDITOR

#endif
}