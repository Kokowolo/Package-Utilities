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

using UnityEngine;
using UnityEditor;

namespace Kokowolo.Utilities.Editor
{
    public class CreateNewMonoBehaviour : MonoBehaviour
    {
        /************************************************************/
        #region Fields

        const string PackageName = "com.kokowolo.utilities";
        const string NewMonoBehaviourName = "NewMonoBehaviour";

        #endregion
        /************************************************************/
        #region Menu Item Functions

        [MenuItem(itemName: "Kokowolo/Create/C# Script", isValidateFunction: false, priority: 50)]
        [MenuItem(itemName: "Assets/Create/C# Script - Kokowolo", isValidateFunction: false, priority: 50)]
        public static void CreateScriptAssetFromScriptTemplates()
        {
            CreateNewScript.CreateScriptAssetFromName(PackageName, NewMonoBehaviourName);
        }

        #endregion
        /************************************************************/
    }
}
