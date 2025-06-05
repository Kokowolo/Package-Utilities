/*
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 9, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class LayerMaskUtils
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static LayerMask LayerToLayerMask(int layer)
        {
            return 1 << layer;
        }

        public static int LayerMaskToLayer(LayerMask layerMask)
        {
            return ToLayer(layerMask.value);
        }

        public static int ToLayer(int bitmask) 
        {
            UnityEngine.Assertions.Assert.IsFalse(ContainsMultipleLayers(bitmask), 
                $"{nameof(LayerMaskToLayer)} was passed an invalid mask containing multiple layers");
    
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
        /*██████████████████████████████████████████████████████████*/
    }
}