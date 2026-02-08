/* 
 * Copyright (c) 2026 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: February 6, 2026
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public interface IPoolableMonoBehaviour : IPoolable
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public Transform transform { get; }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions
        
        void IPoolable.OnAddedToPool()
        {
            transform.SetParent(PoolManager.Instance.transform);
        }

        void IPoolable.OnRemovedFromPool()
        {
            transform.SetParent(null);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}