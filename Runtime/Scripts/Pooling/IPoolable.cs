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
    /// <summary>
    /// A poolable class or struct. NOTE: there is no factory or manager for this to be compatible with out of the box
    /// </summary>
    public interface IPoolable
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions
        
        /// <summary>
        /// Called when IPoolable is added to pool
        /// </summary>
        public void OnAddedToPool();

        /// <summary>
        /// Called when IPoolable is created or removed from pool
        /// </summary>
        public void OnRemovedFromPool();
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}