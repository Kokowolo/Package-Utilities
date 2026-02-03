/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 3, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class LayerMaskExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static bool Contains(this LayerMask layerMask, GameObject gameObject) => Contains(layerMask, gameObject.layer);
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            return BitmaskUtils.Contains(layerMask, layer);
        }

        public static bool Contains(this LayerMask layerMaskA, LayerMask layerMaskB)
        {
            return (layerMaskA & layerMaskB) == layerMaskB;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Utilities

        public static LayerMask LayerToLayerMask(int layer)
        {
            return 1 << layer;
        }

        public static int ToLayer(this LayerMask layerMask) => ToLayer(layerMask.value);
        public static int ToLayer(int bitmask) 
        {
            UnityEngine.Assertions.Assert.IsFalse(ContainsMultipleLayers(bitmask), 
                $"{nameof(ToLayer)} was passed an invalid mask containing multiple layers");
    
            int result = bitmask > 0 ? 0 : 31;
            while(bitmask > 1)
            {
                bitmask >>= 1;
                result++;
            }
            return result;
        }

        public static bool ContainsMultipleLayers(LayerMask layerMask)
        {
            return ContainsMultipleLayers(layerMask.value);
        }
        
        public static bool ContainsMultipleLayers(int bitmask)
        {
            return (bitmask & (bitmask - 1)) != 0;
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}