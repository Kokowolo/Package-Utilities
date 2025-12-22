/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 4, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    public static class AnimatorExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        /*——————————————————————————————————————————————————————————*/
        #region Extensions

        public static void Play(this Animator animator, int layer, bool prewarm)
        {
            animator.Play(0, layer, prewarm ? UnityEngine.Random.Range(0f, 1f) : 0);
        }

        public static void PlayPrewarmed(this Animator animator, string stateName, int layer, bool prewarm)
        {
            animator.Play(stateName, layer, prewarm ? UnityEngine.Random.Range(0f, 1f) : 0);
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