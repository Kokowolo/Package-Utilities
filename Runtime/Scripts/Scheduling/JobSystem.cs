/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 23, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kokowolo.Utilities;

namespace Kokowolo.Utilities.Scheduling
{
    public static class JobSystem
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public static bool IsInitialized => JobManager.IsInitialized;

        public static bool IsFree
        {
            get
            {
                bool isFree = true;
                for (int i = 0; isFree && i < JobManager.Instance.JobSchedulers.Count; i++)
                {
                    isFree &= JobManager.Instance.JobSchedulers[i].IsFree;
                }
                return isFree;
            }
        }

        public static int SchedulersCount => JobManager.Instance.JobSchedulers.Count;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}