/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 8, 2025
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

namespace Kokowolo.Utilities
{
    [Serializable]
#if UNITY_EDITOR
    public class WeightedList<T> : IEnumerable<T>, ISerializationCallbackReceiver
#else
    public class WeightedList<T> : IEnumerable<T>
#endif
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        [SerializeField] List<WeightedListItem> items;
        [SerializeField, HideInInspector] float totalWeight;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public int Count => items.Count;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public WeightedList()
        {
            items = new List<WeightedListItem>();
        }

        public T GetRandomItem()
        {
            // Check if empty
            if (items == null || items.Count == 0)
            {
                return default;
            }

            // Return first index if 1
            if (items.Count == 1) return items[0].value;

            float roll = UnityEngine.Random.value * totalWeight;

            for (int i = 0; i < items.Count; i++)
            {
                if (roll <= items[i].weight)
                {
                    return items[i].value;
                }
                roll -= items[i].weight;
            }

            throw new Exception("exceeded bounds of weighted list");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < items.Count; i++)
            {
                yield return items[i].value;
            }
        }

        void UpdateSerialization()
        {
            totalWeight = 0;
            if (items == null || items.Count == 0) return;

            totalWeight = 0f;
            for (int i = 0; i < items.Count; i++)
            {
                totalWeight += items[i].weight;
            }

            for (int i = 0; i < items.Count; i++)
            {
                float percent = totalWeight > 0f ? 
                    items[i].weight / totalWeight * 100f : 
                    i == 0 ? 100 : 0f;
                items[i].SetPercent(percent);
            }
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Subclass

        [Serializable]
        class WeightedListItem
        {
            public T value;
            [Min(0)] public float weight;
            [SerializeField, ReadOnly] public float percent;

            public WeightedListItem(T value, float weight)
            {
                this.value = value;
                this.weight = weight;
                this.percent = 0;
            }

            public void SetPercent(float value) => percent = value;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Editor
#if UNITY_EDITOR

        public void OnBeforeSerialize()
        {
            UpdateSerialization();
        }

        public void OnAfterDeserialize()
        {
            // UpdateSerialization();
        }

#endif
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}