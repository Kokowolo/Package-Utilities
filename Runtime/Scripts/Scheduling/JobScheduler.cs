/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 22, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities.Scheduling
{
    public class JobScheduler : IDisposable
    {
        /*██████████████████████████████████████████████████████████*/
        #region Events

        internal event EventHandler OnEnable;
        internal event EventHandler OnDispose;
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        Queue<Job> pendingJobs;
        Queue<Job> scheduledJobs;
        List<Job> activeJobs;

        // bool enabled = false;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public static JobScheduler Main => 
            JobManager.Instance && JobManager.Instance.JobSchedulers.Count > 0 ? JobManager.Instance.JobSchedulers[0] : null;

        bool _Enabled = false;
        public bool Enabled 
        {
            get => _Enabled;
            internal set
            {
                if (!_Enabled && value) 
                {
                    _Enabled = value;
                    OnEnable?.Invoke(this, EventArgs.Empty);
                }
                _Enabled = value;
            }
        }

        public bool IsFree => pendingJobs.Count == 0 && scheduledJobs.Count == 0 && !IsRunning;
        bool IsRunning => activeJobs.Count != 0;
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        bool disposed;
        ~JobScheduler() => Dispose();
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            OnDispose?.Invoke(this, EventArgs.Empty);
            // StopAllCoroutines(); // TODO: 

            OnEnable = null;
            OnDispose = null;

            pendingJobs = null;
            scheduledJobs = null;
            ListPool.Release(ref activeJobs);
        }

        public static JobScheduler Create()
        {
            return new JobScheduler();
        }
        
        JobScheduler()
        {
            pendingJobs = new Queue<Job>();
            scheduledJobs = new Queue<Job>();
            activeJobs = ListPool.Get<Job>();

            JobManager.Instance.AddScheduler(this);
        }

        internal void Update()
        {
            if (!Enabled) return;
            Enabled = false;

            // Prevent newly created jobs from intefering with this update
            // var pendingJobs = new Queue<Job>();
            // foreach (var i in this.pendingJobs)
            // {
            //     pendingJobs.Enqueue(i);
            // }
            // this.pendingJobs.Clear();

            // Handle/start pending jobs
            while (pendingJobs.Count > 0)
            {
                Job job = pendingJobs.Dequeue();
                if (job.IsPending) 
                {
                    job.IsPending = false;
                    if (job.IsScheduled)
                    {
                        Schedule(job);
                    }
                    else
                    {
                        StartJob(job);
                    }
                }
            }
        }

        internal void PendJob(Job job)
        {
            job.IsPending = true;
            pendingJobs.Enqueue(job);
            Enabled = true;
        }
        
        void StartJob(Job job)
        {
            // job.IsScheduled = false;
            job.OnDispose += Handle_Job_OnDispose;
            activeJobs.Add(job);
            job.Start();
        }

        void Schedule(Job job)
        {   
            scheduledJobs.Enqueue(job);
            job = scheduledJobs.Peek();
            if (!job.IsRunning)
            {
                StartJob(job);
            }
        }

        void Handle_Job_OnDispose(Job job)
        {
            // Handle if active job
            if (!activeJobs.Remove(job)) throw new Exception("impossible state reached");

            // Handle if running scheduled job
            if (!job.IsScheduled) return;

            // Handle next scheduled job
            if (scheduledJobs.Count == 0) throw new Exception("impossible state reached");
            scheduledJobs.Dequeue();
            if (scheduledJobs.Count == 0) return;
            StartJob(scheduledJobs.Peek());
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}