/*
 * File Name: GameObjectExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 17, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    /************************************************************/
    #region Functions

    public static bool IsPrefab(this GameObject gameObject)
    {
        return gameObject.scene.name == null;
    }
    
    #endregion
    /************************************************************/
}