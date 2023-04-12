/*
 * File Name: IPoolable.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 9, 2023
 * 
 * Additional Comments:
 *		File Line Length: 120
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public interface IPoolable<T> 
    {
        /************************************************************/
        #region Functions

        // NOTE: PoolSystem will call Create() if there are no IPoolable<T> in the stack
        // public abstract static T Create();

        public abstract void OnAddPoolable();

        public abstract void OnGetPoolable();
        
        #endregion
        /************************************************************/
    }
}