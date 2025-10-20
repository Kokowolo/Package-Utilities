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
    public class JobScheduler : IEquatable<JobScheduler>, IDisposable
    {
        /*██████████████████████████████████████████████████████████*/
        #region Events

        internal event EventHandler OnEnable;
        internal event EventHandler OnDispose;

        public event Action OnFree;
        
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

        static int id = 0;
        internal int instanceId;

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
        internal bool IsDisposed => disposed;
        bool IsRunning => activeJobs.Count != 0;

        /// <summary>
        /// Total number of active jobs; a job is considered active if it's not disposed
        /// </summary>
        public int ActiveJobCount => pendingJobs.Count + scheduledJobs.Count + activeJobs.Count;
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static JobScheduler Create()
        {
            JobScheduler jobScheduler = new JobScheduler();
            JobManager.Instance.AddScheduler(jobScheduler);
            return jobScheduler;
        }
        
        JobScheduler()
        {
            instanceId = id++;
            pendingJobs = new Queue<Job>();
            scheduledJobs = new Queue<Job>();
            activeJobs = ListPool.Get<Job>();
        }

        bool disposed;
        ~JobScheduler() => Dispose();
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            StopAllJobs();

            bool isJobSystemScheduler = JobSystem.GetScheduler() == this;
            OnDispose?.Invoke(this, EventArgs.Empty);

            OnEnable = null;
            OnDispose = null;
            OnFree = null;

            pendingJobs = null;
            scheduledJobs = null;
            ListPool.Release(ref activeJobs);
            if (isJobSystemScheduler) JobSystem.SetScheduler(null);
        }

        internal void Update() // NOTE: called from JobSystem, not via MonoBehaviour
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

        public void StopAllJobs()
        {
            for (int i = pendingJobs.Count - 1; i >= 0 ; i--)
            {
                pendingJobs.Dequeue().Dispose();
            }
            for (int i = scheduledJobs.Count - 1; i >= 0 ; i--)
            {
                scheduledJobs.Dequeue().Dispose();
            }
            for (int i = activeJobs.Count - 1; i >= 0 ; i--)
            {
                activeJobs[i].OnDispose -= Handle_Job_OnDispose;
                activeJobs[i].Dispose();
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
            if (job.IsScheduled)
            {
                // Handle next scheduled job
                if (scheduledJobs.Count == 0) throw new Exception("impossible state reached");
                scheduledJobs.Dequeue();
                if (scheduledJobs.Count != 0)
                {
                    StartJob(scheduledJobs.Peek());
                }
            }
            if (IsFree) OnFree?.Invoke();
        }

        public bool Equals(JobScheduler other) => this == other;
        public override bool Equals(object obj) => Equals(obj as Job);
        public static bool operator !=(JobScheduler a, JobScheduler b) => !(a == b);
        public static bool operator ==(JobScheduler a, JobScheduler b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null) return false;
            if (b is null) return false;
            return a.instanceId == b.instanceId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(instanceId);
        }

        public override string ToString()
        {
            return $"{nameof(JobScheduler)}:{instanceId}";
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}