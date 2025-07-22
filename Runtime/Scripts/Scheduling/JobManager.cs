/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 15, 2025
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
    public class JobManager : MonoBehaviourSingleton<JobManager>
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        internal List<JobScheduler> JobSchedulers;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        protected override void Singleton_OnDestroy()
        {
            for (int i = JobSchedulers.Count - 1; i >= 0 ; i--)
            {
                JobSchedulers[i].Dispose();
            }
        }

        protected override void Singleton_Awake()
        {
            JobSchedulers = new List<JobScheduler>();
            JobScheduler.Create();
        }

        internal void RemoveScheduler(JobScheduler scheduler)
        {
            if (JobSchedulers.IndexOf(scheduler) == -1)
            {
                LogManager.LogException($"attempting to remove {nameof(JobScheduler)} that does not exist in {nameof(JobManager)}");
                return;
            }

            JobSchedulers.Remove(scheduler);
            scheduler.OnEnable -= Handle_JobScheduler_OnEnable;
        }

        internal void AddScheduler(JobScheduler scheduler)
        {
            if (JobSchedulers.IndexOf(scheduler) != -1)
            {
                LogManager.LogException($"attempting to add {nameof(JobScheduler)} that already exists in {nameof(JobManager)}");
                return;
            }

            JobSchedulers.Add(scheduler);
            scheduler.OnEnable += Handle_JobScheduler_OnEnable;
            scheduler.OnDispose += Handle_JobScheduler_OnDispose;
        }

        void LateUpdate()
        {
            enabled = false;
            for (int i = 0; i < JobSchedulers.Count; i++)
            {
                JobSchedulers[i].Update();
            }
        }

        void Handle_JobScheduler_OnEnable(object sender, EventArgs e)
        {
            enabled = true;
        }

        void Handle_JobScheduler_OnDispose(object sender, EventArgs e)
        {
            RemoveScheduler(sender as JobScheduler);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}