/*
 * File Name: CreateNewMonoBehaviour.cs
 * Description: This script is for creating new MonoBehaviour Scripts within Kokowolo Projects; for more info, see 
 *              CreateNewScript.cs
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 26, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    public static class CreateNewMonoBehaviour
    {
        /************************************************************/
        #region Fields

        const string PackageName = "com.kokowolo.utilities";

        #endregion
        /************************************************************/
        #region Menu Item Functions

        [MenuItem(itemName: "Kokowolo/Create/C# Script - Simple", isValidateFunction: false, priority: 50)]
        [MenuItem(itemName: "Assets/Create/C# Script - Simple", isValidateFunction: false, priority: 50)]
        public static void CreateSimpleScript()
        {
            CreateNewScript.CreateFileFromPackageTemplateFile(PackageName, $"NewBehaviourSimple.cs.txt");
        }

        [MenuItem(itemName: "Kokowolo/Create/C# Script - Verbose", isValidateFunction: false, priority: 51)]
        [MenuItem(itemName: "Assets/Create/C# Script - Verbose", isValidateFunction: false, priority: 51)]
        public static void CreateVerboseScript()
        {
            CreateNewScript.CreateFileFromPackageTemplateFile(PackageName, $"NewBehaviourVerbose.cs.txt");
        }

        #endregion
        /************************************************************/
    }
}
