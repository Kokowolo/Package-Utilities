/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 27, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class AsyncOperationExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static void OnCompletedOrIfNull(this AsyncOperation operation, Action<AsyncOperation> completed)
        {
            if (operation == null)
            {
                completed?.Invoke(null);
            }
            else
            {
                operation.completed += completed;
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}