/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: June 5, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        static T _Instance;
        public static T Instance
        {
            get
            {
                if (!_Instance)
                {
                    _Instance = PrefabManager.Get<T>();
                }
                return _Instance;
            }
        }

//             // TODO: move this to ScriptableObjectSingleton?
//         public static T Instance 
//         {
//             get
//             {
// #if UNITY_EDITOR
//                 // TODO: move to EditorOnlyUtils within Kokowolo.Utilities and surround with UNITY_EDITOR compiler directive
//                 Kokowolo.Utilities.EditorOnly.EditorUtils.FindFirstAssetByType<T>()
// #else
//                 return PrefabManager.Get<Struggle>();
// #endif
//             }
//         }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}