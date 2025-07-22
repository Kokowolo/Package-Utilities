/* 
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

        public override bool keepWaiting => !scheduler.IsFree;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public WaitForJobScheduler() : this(JobScheduler.Main) {}
        public WaitForJobScheduler(JobScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}