/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: January 17, 2022
 * 
 * Additional Comments:
 *		File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class GameObjectExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        [System.Obsolete("use `LayerMaskExtensions.ContainsGameObject(GameObject)` instead")]
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            return layerMask.ContainsGameObject(gameObject);
        }

        public static bool IsTheOriginalPrefab(this GameObject gameObject)
        {
            // NOTE: UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage()
            return gameObject.scene.name == null;
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}