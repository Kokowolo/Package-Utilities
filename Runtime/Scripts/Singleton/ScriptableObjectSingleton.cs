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
        #region Settings

#if UNITY_EDITOR
        [Header("Editor Settings: Singleton")]
        [SerializeField] bool findFirstAssetByTypeWhileInEditor;
#endif

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        static T _Instance;
        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (!_Instance)
                    {
                        T asset = Editor.EditorOnlyUtils.FindFirstAssetByType<T>();
                        ScriptableObjectSingleton<T> singleton = asset as ScriptableObjectSingleton<T>;
                        if (singleton.findFirstAssetByTypeWhileInEditor) 
                        {
                            LogManager.Log($"{typeof(T)} instance set");
                            _Instance = asset;
                        }
                    }
                }
#endif
                if (!_Instance)
                {
                    _Instance = PrefabManager.Get<T>();
                }
                return _Instance;
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}