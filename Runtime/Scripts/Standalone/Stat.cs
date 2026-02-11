/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: August 29, 2023
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Kokowolo.Utilities
{
    /// <summary>
    /// Attribute for Stat class to specify the ReadOnly nature of its max value
    /// </summary>
    public class StatReadOnlyMaxAttribute : PropertyAttribute
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties
        
        public bool ReadOnlyMax { get; set; } = true;

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }

    [Serializable]
#if UNITY_EDITOR
    public class Stat : ISerializationCallbackReceiver
#else
    public class Stat
#endif
    {
        /*██████████████████████████████████████████████████████████*/
        #region Enums

        public enum UpdateStateType
        {
            None = -1,
            Now,
            NextFrame,
        }

        enum StatStatus
        {
            None = -1,
            Healthy,
            Depleting, // at 0 but not Depleted
            Depleted
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Events

        public event Action OnChanged;
        public event Action OnDepleted;
        public event Action OnRevived;
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields
        
        [SerializeField] float current = 1;
        [SerializeField] float max = 1;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public string Name { get; private set; } = "Stat";

        public float CurrentValue => current;
        public float MaxValue => max;
        /// <summary>
        /// Stat as a value between 0 and 1
        /// </summary>
        public float NormalizedValue => current / max;
        bool IsPositive => current > 0;
        StatStatus Status { get; set; } = StatStatus.None;

        public bool IsHealthy => Status == StatStatus.Healthy;
        public bool IsDepleting => Status == StatStatus.Depleting;
        public bool IsDepleted => Status == StatStatus.Depleted;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public Stat(Stat statHandler) : this(statHandler.Name, statHandler.current, statHandler.max) {}
        public Stat(float maxValue) : this("Stat", maxValue, maxValue) {}
        public Stat(float currentValue, float maxValue) : this("Stat", currentValue, maxValue) {}
        public Stat(string name, float currentValue, float maxValue)
        {
            this.Name = name;
            this.max = maxValue;
            this.current = currentValue;
            RefreshStatus();
        }

        public void UpdateState()
        {
            LogManager.Log($"{Name} called {nameof(UpdateState)}");
            if (Status == StatStatus.Depleting) 
            {
                Deplete();
            }
            else if (IsPositive && Status == StatStatus.Depleted) 
            {
                Revive();
            }
        }

        void RefreshStatus()
        {
            // don't set status if this stat is already depleted (must call Revive)
            Status = Status == StatStatus.Depleted ? StatStatus.Depleted : IsPositive ? StatStatus.Healthy : StatStatus.Depleting;
        }

        [Obsolete("use Set now, which swizzles max and current values")]
        public void SetStat(float max, float current, UpdateStateType updateStateType = UpdateStateType.Now) => Set(current, max, updateStateType);
        public void Set(float current, float max, UpdateStateType updateStateType = UpdateStateType.Now)
        {
            if (current == this.current && this.max == max) return;

            // NOTE: Stat cannot have negative values, is this okay?
            this.current = Mathf.Clamp(current, 0, this.max);
            this.max = Mathf.Max(0, max);

            RefreshStatus();
            OnChanged?.Invoke();

            switch (updateStateType)
            {
                case UpdateStateType.None:
                    // nada, expecting a manual call later
                    break; 
                case UpdateStateType.Now:
                    UpdateState();
                    break;
                case UpdateStateType.NextFrame:
                    _ScheduleUpdateForNextFrame();
                    void _ScheduleUpdateForNextFrame() => Kokowolo.Utilities.Scheduling.Job.Add(UpdateState); // declare local function
                    break;   
            }
        }

        public void ChangeMaxValueBy(float amount, UpdateStateType updateStateType = UpdateStateType.Now)
        {
            Set(current + amount, max + amount, updateStateType);
        }

        public void ChangeValueBy(float amount, UpdateStateType updateStateType = UpdateStateType.Now)
        {
            Set(current + amount, max, updateStateType);
        }

        public void Deplete()
        {
            if (Status == StatStatus.Depleted) return;
            Status = StatStatus.Depleted;
            Set(0, max, updateStateType: UpdateStateType.None);
            LogManager.Log($"{Name} has depleted");
            OnDepleted?.Invoke();
        }

        public void Revive()
        {
            if (Status != StatStatus.Depleted) return;

            Status = IsPositive ? StatStatus.Healthy : StatStatus.Depleting;
            LogManager.Log($"{Name} has revived");
            OnRevived?.Invoke();
        }

        public void Replenish()
        {
            Revive();
            Set(max, max, updateStateType: UpdateStateType.None);
            LogManager.Log($"{Name} has replenished");
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Editor
#if UNITY_EDITOR
        
        public void OnBeforeSerialize()
        {
            if (current > max)
            {
                current = max;
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
        
#endif
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}