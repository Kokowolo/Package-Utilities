/* 
 * Copyright (c) 2025 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: July 15, 2025
 * 
 * Additional Comments:
 *      File Line Length: ~140
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities.Scheduling
{
    public class WaitForJobScheduler : CustomYieldInstruction
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        JobScheduler scheduler;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public override bool keepWaiting => scheduler != null && !scheduler.IsDisposed && !scheduler.IsFree;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public WaitForJobScheduler() : this(JobSystem.GetScheduler()) {}
        public WaitForJobScheduler(JobScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}