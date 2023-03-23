/*
 * File Name: FileManager.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 13, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

namespace Kokowolo.Utilities
{
    public static class FileManager
    {
        /************************************************************/
        #region Fields

        const string Extension = "bytes";

        #endregion
        /************************************************************/
        #region Properties

        #endregion
        /************************************************************/
        #region Functions

        public static List<string> GetFilePaths(string path, string extension = null)
        {
            List<string> filePaths;
            if (extension == null) 
            {
                filePaths = new List<string>(Directory.GetFiles(path));
                LogManager.LogWarning("No extension specified, be careful when working with possible hidden files");
            }
            else 
            {
                // TODO: this will throw an error if this doesn't exist
                filePaths = new List<string>(Directory.GetFiles(path, $"*.{extension}"));
            }

            return filePaths;
        }

        // TODO: make private; other classes should just be able to directly open/load TextAssets
        public static Stream GetByteStream(TextAsset asset)
        {
            if (!asset) return null;
            return new MemoryStream(asset.bytes);
        }

        // TODO: make private; other classes should just be able to directly open/load TextAssets
        public static bool IsPathValid(string path)
        {
            if (!File.Exists(path))
            {
                LogManager.LogError("Path/File does not exist " + path);
                return false;
            }
            return true;
        }

        // public static void Test()
        // {
        //     string path = Application.persistentDataPath;
        //     foreach (var file in Directory.GetFiles(path, "*.bytes")) Debug.Log(file);
        // }

        #endregion
        /************************************************************/
    }
}