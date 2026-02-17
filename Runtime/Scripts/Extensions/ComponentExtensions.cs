/*
 * Copyright (c) 2026 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: February 17, 2026
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class ComponentExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static T CacheGetComponent<T>(this Component self, ref T component) where T : Component
        {
            if (!component) 
            {
                component = self.GetComponent<T>();
            }
            return component;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        #endregion
        /*——————————————————————————————————————————————————————————*/
        
        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}