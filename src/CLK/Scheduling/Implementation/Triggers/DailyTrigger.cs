using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class DailyTrigger : ITaskTrigger
    {
        // Fields
        private readonly IEnumerable<DailyTime> _dailyTimeCollection = null;


        // Constructors
        public DailyTrigger(DailyTime dailyTime)
        {
            #region Contracts

            if (dailyTime == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _dailyTimeCollection = new DailyTime[] { dailyTime };
        }

        public DailyTrigger(IEnumerable<DailyTime> dailyTimeCollection)
        {
            #region Contracts

            if (dailyTimeCollection == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _dailyTimeCollection = dailyTimeCollection;
        }


        // Methods
        public bool Approve(DateTime executeTime, DateTime lastExecuteTime)
        {
            // Approve
            for (int offsetDay = 0; offsetDay <= 1; offsetDay++)
            {
                foreach (var dailyTime in _dailyTimeCollection)
                {
                    // Next
                    var nextExecuteTime = dailyTime.Next(lastExecuteTime, offsetDay);
                    if (nextExecuteTime.HasValue == false) continue;

                    // Check
                    if (nextExecuteTime.Value > lastExecuteTime)
                    {
                        if (nextExecuteTime.Value <= executeTime)
                        {
                            return true;
                        }
                    }
                }
            }

            // Return
            return false;
        }
    }

    public sealed class DailyTime
    {
        // Constructors
        public DailyTime(int hour, int minute)
        {
            // Require
            if (0 > hour || hour > 23) throw new ArgumentException();
            if (0 > minute || minute > 59) throw new ArgumentException();

            // Arguments
            this.Hour = hour;
            this.Minute = minute;
        }


        // Properties
        public int Hour { get; private set; }

        public int Minute { get; private set; }


        // Methods
        internal DateTime? Next(DateTime lastExecuteTime, int offsetDay)
        {
            // Next
            var nextExecuteTime = new DateTime(lastExecuteTime.Year, lastExecuteTime.Month, lastExecuteTime.Day, 0, 0, 0);
            nextExecuteTime = nextExecuteTime.AddDays(offsetDay);
            nextExecuteTime = nextExecuteTime.AddHours(this.Hour);
            nextExecuteTime = nextExecuteTime.AddMinutes(this.Minute);

            // Return
            return nextExecuteTime;
        }
    }
}
