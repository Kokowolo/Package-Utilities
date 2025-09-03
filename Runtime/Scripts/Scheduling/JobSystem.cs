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
        #region Events

        // public static event Action OnFree; // TODO: hook this up to scheduler's free event, fire when IsFree

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields
        
        static JobScheduler scheduler;
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public static bool IsInitialized => JobManager.IsInitialized;

        public static bool IsFree
        {
            get
            {
                if (!JobManager.IsInitialized) return true;
                bool isFree = true;
                for (int i = 0; isFree && i < JobManager.Instance.JobSchedulers.Count; i++)
                {
                    isFree &= JobManager.Instance.JobSchedulers[i].IsFree;
                }
                return isFree;
            }
        }

        public static int SchedulerCount => JobManager.IsInitialized ? JobManager.Instance.JobSchedulers.Count : 0;
        
        /// <summary>
        /// Total number of active jobs; a job is considered active if it's not disposed
        /// </summary>
        public static int ActiveJobCount
        {
            get 
            {
                if (!JobManager.IsInitialized) return 0;
                int count = 0;
                for (int i = 0; i < JobManager.Instance.JobSchedulers.Count; i++)
                {
                    count += JobManager.Instance.JobSchedulers[i].ActiveJobCount;
                }
                return count;
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /// <summary>
        /// Gets the default JobScheduler that is used by the JobSystem when a JobScheduler is not specified
        /// </summary>
        public static JobScheduler GetScheduler() // NOTE: this is not a property because this way we prevent auto serialization 
        {
            if (scheduler == null)
            {
                if (JobManager.Instance.JobSchedulers.Count == 0) JobScheduler.Create();
                scheduler = JobManager.Instance.JobSchedulers[0];
            }
            return scheduler;
        }

        /// <summary>
        /// Sets the default JobScheduler that is used by the JobSystem when a JobScheduler is not specified
        /// </summary>
        public static void SetScheduler(JobScheduler scheduler)
        {
            JobSystem.scheduler = scheduler;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}