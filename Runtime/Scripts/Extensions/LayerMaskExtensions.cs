/*
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

        public static bool ContainsGameObject(this LayerMask layerMask, GameObject gameObject)
        {
            return ContainsLayer(layerMask, gameObject.layer);
        }

        public static bool ContainsLayer(this LayerMask layerMask, int layer)
        {
            // NOTE: 1 is shifted `layer` times; 000000001 -> 000010000 for layer 4
            return (layerMask & (1 << layer)) != 0;
        }

        public static bool ContainsLayerMask(this LayerMask layerMaskA, LayerMask layerMaskB)
        {
            return (layerMaskA & layerMaskB) == layerMaskB;
        }

        public static int ToLayer(this LayerMask layerMask)
        {
            return LayerMaskUtils.LayerMaskToLayer(layerMask);
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}