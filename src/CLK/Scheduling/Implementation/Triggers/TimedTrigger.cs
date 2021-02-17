using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class TimedTrigger : ITaskTrigger
    {
        // Fields
        private readonly IEnumerable<DateTime> _timeCollection = null;


        // Constructors
        public TimedTrigger(DateTime time)
        {
            // Arguments
            _timeCollection = new DateTime[] { time };
        }

        public TimedTrigger(IEnumerable<DateTime> timeCollection)
        {
            #region Contracts

            if (timeCollection == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _timeCollection = timeCollection;
        }


        // Methods
        public bool Approve(DateTime executeTime, DateTime lastExecuteTime)
        {
            // Approve
            foreach (var nextExecuteTime in _timeCollection)
            {
                // Check
                if (nextExecuteTime > lastExecuteTime)
                {
                    if (nextExecuteTime <= executeTime)
                    {
                        return true;
                    }
                }
            }

            // Return
            return false;
        }
    }
}
