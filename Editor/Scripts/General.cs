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

using System.Linq;
using UnityEditor.PackageManager;

namespace Kokowolo.Utilities.Editor
{
    public static class General
    {
        /************************************************************/
        #region Properties

        #endregion
        /************************************************************/
        #region Functions

        public static PackageInfo GetPackageInfo(string packageName)
        {
            return UnityEditor.AssetDatabase.FindAssets("package")
                .Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
                    .Where(x => UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath)
                    .Where(x => x != null)
                .First(x => x.name == packageName);
        }

        #endregion
        /************************************************************/
    }
}