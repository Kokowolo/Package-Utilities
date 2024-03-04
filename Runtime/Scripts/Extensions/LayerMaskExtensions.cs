/*
 * File Name: LayerMaskExtensions.cs
 * Description: This script is for ...
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 3, 2024
 * 
 * Additional Comments:
 *		File Line Length: 140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskExtensions
{
    /************************************************************/
    #region Functions

    public static bool Contains(this LayerMask layerMask, GameObject gameObject)
    {
        return Contains(layerMask, gameObject.layer);
    }

    public static bool Contains(this LayerMask layerMaskA, LayerMask layerMaskB)
    {
        return (layerMaskA & (1 << layerMaskB)) != 0;
    }

    #endregion
    /************************************************************/
}