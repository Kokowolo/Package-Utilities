/* 
 * Copyright (c) 2026 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: October 16, 2026
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Kokowolo.Utilities
{
    public static class TilemapExtensions
    {
        public static IEnumerable<Vector3Int> ForEachTilePosition(this Tilemap tilemap)
        {
            if (tilemap == null) yield break;
            foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(position)) yield return position;
            }
        }
    }
}