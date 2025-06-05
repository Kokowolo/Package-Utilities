/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 27, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using UnityEngine;
using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    public static class CreateNewGameObjectDivider
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        static GameObject renameGameObject;
        static double renameTime;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        [ MenuItem("GameObject/Create Empty Divider", isValidateFunction: false, priority: 0)]
        public static void CreateEmptyDivider()
        {
            renameGameObject = new GameObject();
            renameGameObject.name = "------ new ------";
            renameGameObject.tag = "EditorOnly";
            renameTime = EditorApplication.timeSinceStartup + 0.2d;

            EditorApplication.update += RenameGameObjectMode;
        }

        private static void RenameGameObjectMode()
        {
            Selection.objects = new Object[] { renameGameObject };

            if (EditorApplication.timeSinceStartup >= renameTime)
            {
                EditorApplication.update -= RenameGameObjectMode;
                EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
                EditorApplication.ExecuteMenuItem("Edit/Rename");
            }
        }
    
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}