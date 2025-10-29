/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 26, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    /// <summary>
    /// Creates new MonoBehaviour Scripts within Kokowolo Projects; for more info, see CreateNewScript.cs
    /// </summary>
    public static class CreateNewMonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        const string PackageName = "com.kokowolo.utilities";

        const int priority = -500;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Menu Item Functions

        [MenuItem(itemName: "Kokowolo/Create/C# Script - Simple", isValidateFunction: false, General.MenuItemPriority)]
        [MenuItem(itemName: "Assets/Create/C# Script - Simple", isValidateFunction: false, General.MenuItemPriority)]
        public static void CreateSimpleScript()
        {
            CreateNewScript.CreateFileFromPackageTemplateFile(PackageName, $"NewBehaviourSimple.cs.txt");
        }

        // [MenuItem(itemName: "Kokowolo/Create/C# Script - Verbose", isValidateFunction: false, priority: -401)]
        // [MenuItem(itemName: "Assets/Create/C# Script - Verbose", isValidateFunction: false, priority: -401)]
        // public static void CreateVerboseScript()
        // {
        //     CreateNewScript.CreateFileFromPackageTemplateFile(PackageName, $"NewBehaviourVerbose.cs.txt");
        // }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}
