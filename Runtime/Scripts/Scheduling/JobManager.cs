/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
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

#if UNITY_EDITOR
using UnityEditor;
#endif

using Kokowolo.Utilities;

namespace Kokowolo.Utilities.Scheduling
{
    [AddComponentMenu("")] // Hide in menu
    internal class JobManager : MonoBehaviour
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        internal static bool IsInitialized => instance; // used to check if Instance exists without lazy init
        static JobManager instance;
        internal static JobManager Instance
        {
            get
            {
                if (!instance)
                {
                    new GameObject($"[Kokowolo.Utilities {nameof(JobManager)}]").AddComponent<JobManager>();
                }
                return instance;
            }
        }

        internal List<JobScheduler> JobSchedulers;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        void Awake()
        {
            if (instance)
            {
                LogManager.LogError("instance already exists, destroying GameObject");
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
            JobSchedulers = new List<JobScheduler>();
        }

        void OnDestroy()
        {
            if (instance != this) return;

            for (int i = JobSchedulers.Count - 1; i >= 0 ; i--)
            {
                JobSchedulers[i].Dispose();
            }
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

#if UNITY_EDITOR
    [CustomEditor(typeof(JobManager))]
    public class JobManagerEditor : UnityEditor.Editor
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        JobManager Target => target as JobManager;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // int pendingJobsCount = 0;
            // int scheduledJobsCount = 0;
            // int activeJobsCount = 0;
            // for (int i = 0; i < Target.JobSchedulers.Count; i++)
            // {
            //     var pendingJobs = Target.JobSchedulers[i].GetType().GetField($"pendingJobs", ReflectionExtensions.AllFlags).GetValue(Target.JobSchedulers[i]) as Queue<Job>;
            //     pendingJobsCount += pendingJobs.Count;
            //     var scheduledJobs = Target.JobSchedulers[i].GetType().GetField("scheduledJobs", ReflectionExtensions.AllFlags).GetValue(Target.JobSchedulers[i]) as Queue<Job>;
            //     scheduledJobsCount += scheduledJobs.Count;
            //     var activeJobs = Target.JobSchedulers[i].GetType().GetField("activeJobs", ReflectionExtensions.AllFlags).GetValue(Target.JobSchedulers[i]) as List<Job>;
            //     activeJobsCount += activeJobs.Count;
            // }
            // GUILayout.Label($"Total Number of Pending Jobs: {pendingJobsCount}");
            // GUILayout.Label($"Total Number of Scheduled Jobs: {scheduledJobsCount}");
            // GUILayout.Label($"Total Number of Active Jobs: {activeJobsCount}");

            for (int i = 0; i < Target.JobSchedulers.Count; i++)
            {
                JobScheduler scheduler = Target.JobSchedulers[i];

                // Show JobScheduler.instanceId
                int instanceId = scheduler.GetType().GetField($"instanceId", ReflectionExtensions.AllFlags).GetValue(scheduler) as int? ?? -1;
                GUILayout.Label($"{nameof(JobScheduler)}_{instanceId} ({scheduler.ActiveJobCount})");
                EditorGUI.indentLevel++;

                // Show pending jobs
                var pendingJobs = scheduler.GetType().GetField($"pendingJobs", ReflectionExtensions.AllFlags).GetValue(scheduler) as Queue<Job>;
                scheduler.Editor_FoldoutPendingJobs = EditorGUILayout.Foldout(scheduler.Editor_FoldoutPendingJobs, $"Pending Jobs ({pendingJobs.Count})");
                if (scheduler.Editor_FoldoutPendingJobs) DrawJobs(pendingJobs);

                // Show scheduled jobs
                var scheduledJobs = scheduler.GetType().GetField($"scheduledJobs", ReflectionExtensions.AllFlags).GetValue(scheduler) as Queue<Job>;
                scheduler.Editor_FoldoutScheduledJobs = EditorGUILayout.Foldout(scheduler.Editor_FoldoutScheduledJobs, $"Scheduled Jobs ({scheduledJobs.Count})");
                if (scheduler.Editor_FoldoutScheduledJobs) DrawJobs(scheduledJobs);

                // Show active jobs
                var activeJobs = scheduler.GetType().GetField($"activeJobs", ReflectionExtensions.AllFlags).GetValue(scheduler) as List<Job>;
                scheduler.Editor_FoldoutActiveJobs = EditorGUILayout.Foldout(scheduler.Editor_FoldoutActiveJobs, $"Active Jobs ({activeJobs.Count})");
                if (scheduler.Editor_FoldoutActiveJobs) DrawJobs(activeJobs);

                // Reset indentation
                EditorGUI.indentLevel--;
            }
        }

        void DrawJobs(IEnumerable<Job> jobs)
        {
            EditorGUI.indentLevel++;
            foreach (var job in jobs)
            {
                job.Editor_Foldout = EditorGUILayout.Foldout(job.Editor_Foldout, $"{job}");
                if (job.Editor_Foldout)
                { 
                    EditorGUI.indentLevel++;
                    GUILayout.Label(job.Editor_StackTrace);
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.indentLevel--;
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
#endif
}
