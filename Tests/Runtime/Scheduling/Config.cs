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

namespace Scheduling
{
    [CreateAssetMenu(menuName = "Kokowolo/Utilities/Tests/Scheduling/Config", fileName = "Config")]
    public class Config : ScriptableObject
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [SerializeField, Min(-0.01f)] float time;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        static Config _Instance;
        public static Config Instance 
        {
            get
            {
                if (!_Instance)
                {
                    _Instance = Kokowolo.Utilities.Editor.EditorOnlyUtils.FindFirstAssetByType<Config>();
                }
                return _Instance;
            }
        }

        public static float Time => Instance.time;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}