#if UNITY_EDITOR
/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 17, 2025
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
    public static class EditorOnlyUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static T FindFirstAssetByType<T>() where T : Object
        {
            var guid = AssetDatabase.FindAssets($"t:{typeof(T).Name}")[0];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}

#endif