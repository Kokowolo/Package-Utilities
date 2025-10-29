/*
 * Copyright (c) 2025 Kokowolo. All Rights Reserved. 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: April 19, 2024
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public class SceneAttribute : PropertyAttribute 
    { 
        public bool EnsureInBuildSettings { get; }

        public SceneAttribute() : this(true) {}
        public SceneAttribute(bool ensureInBuildSettings)
        {
            EnsureInBuildSettings = ensureInBuildSettings;
        }
    }
}