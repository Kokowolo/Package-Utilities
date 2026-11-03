/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 26, 2025
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
    public struct Id : IEquatable<Id>
    {
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        // HACK: this is public but IdValue<T>.Id is not, setting value directly may cause non-unique ids
        [field: NonSerialized] public int Value { get; set; } 

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        // public void Init<T>(T obj) => Init<T>(); // convenience method
        // public void Init<T>()
        // {
        //     if (Value != 0) return;
        //     Value = IdValue<T>.GetValue();
        // }
        
        // NOTE: call this instead of Init
        public static Id Create<T>(T obj) => Create<T>(); // convenience method
        public static Id Create<T>()
        {
            return new Id
            {
                Value = IdValue<T>.GetValue()
            };
        }

        public static void ResetIterator<T>()
        {
            IdValue<T>.Reset();
        }

        public bool Equals(Id other) => this == other;
        public override bool Equals(object obj) => Equals((Id) obj);
        public static bool operator !=(Id a, Id b) => !(a == b);
        public static bool operator ==(Id a, Id b)
        {
            return a.Value == b.Value;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public override string ToString() => $"{nameof(Id)}{Value}";

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Subclasses

        struct IdValue<T>
        {
            static int value;

            public static void Reset()
            {
                value = 0;
            }

            public static int GetValue()
            {
                return ++value;
            }
        }
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}