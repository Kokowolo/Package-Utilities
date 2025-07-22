#if DOTWEEN

/* 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 22, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Kokowolo.Utilities.Scheduling
{
    public partial class Job
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Job WaitWhile(Tween tween) => Get(Utils.WaitWhile(tween.IsActive));
        public static Job ScheduleWaitWhile(Tween tween) => Schedule(Utils.WaitWhile(tween.IsActive));

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }

    public partial class JobSequence
    {
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public Job PrependWaitWhile(Tween tween) => Prepend(Utils.WaitWhile(tween.IsActive));
        public Job AppendWaitWhile(Tween tween) => Append(Utils.WaitWhile(tween.IsActive));

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}

#endif