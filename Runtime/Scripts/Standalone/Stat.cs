/* 
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
    public class Stat
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
        
        [SerializeField] float maxValue = 1;
        [SerializeField] float currentValue = 1;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public string Name { get; private set; } = "Stat";

        public float MaxValue => maxValue;
        public float CurrentValue => currentValue;
        /// <summary>
        /// Stat as a value between 0 and 1
        /// </summary>
        public float NormalizedValue => currentValue / maxValue;
        bool IsPositive => currentValue > 0;
        StatStatus Status { get; set; } = StatStatus.None;

        public bool IsHealthy => Status == StatStatus.Healthy;
        public bool IsDepleting => Status == StatStatus.Depleting;
        public bool IsDepleted => Status == StatStatus.Depleted;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public Stat(Stat statHandler) : this(statHandler.Name, statHandler.maxValue, statHandler.currentValue) {}
        public Stat(float maxValue) : this("Stat", maxValue, maxValue) {}
        public Stat(float maxValue, float currentValue) : this("Stat", maxValue, currentValue) {}
        public Stat(string name, float maxValue, float currentValue)
        {
            this.Name = name;
            this.maxValue = maxValue;
            this.currentValue = currentValue;
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

        public void SetStat(float max, float current, UpdateStateType updateStateType = UpdateStateType.Now)
        {
            if (maxValue == max && current == currentValue) return;

            // NOTE: StatHandler cannot have negative values, is this okay?
            maxValue = Mathf.Max(0, max);
            currentValue = Mathf.Clamp(current, 0, maxValue);

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
            SetStat(maxValue + amount, currentValue + amount, updateStateType);
        }

        public void ChangeValueBy(float amount, UpdateStateType updateStateType = UpdateStateType.Now)
        {
            SetStat(maxValue, currentValue + amount, updateStateType);
        }

        public void Deplete()
        {
            if (Status == StatStatus.Depleted) return;
            Status = StatStatus.Depleted;
            SetStat(maxValue, 0, updateStateType: UpdateStateType.None);
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
            SetStat(maxValue, maxValue, updateStateType: UpdateStateType.None);
            LogManager.Log($"{Name} has replenished");
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}