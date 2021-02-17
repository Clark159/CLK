using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class HourlyTrigger : ITaskTrigger
    {
        // Fields
        private readonly IEnumerable<HourlyTime> _hourlyTimeCollection = null;


        // Constructors
        public HourlyTrigger(HourlyTime hourlyTime)
        {
            #region Contracts

            if (hourlyTime == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _hourlyTimeCollection = new HourlyTime[] { hourlyTime };
        }

        public HourlyTrigger(IEnumerable<HourlyTime> hourlyTimeCollection)
        {
            #region Contracts

            if (hourlyTimeCollection == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _hourlyTimeCollection = hourlyTimeCollection;
        }


        // Methods
        public bool Approve(DateTime executeTime, DateTime lastExecuteTime)
        {
            // Approve
            for (int offsetHour = 0; offsetHour <= 1; offsetHour++)
            {
                foreach (var hourlyTime in _hourlyTimeCollection)
                {
                    // Next
                    var nextExecuteTime = hourlyTime.Next(lastExecuteTime, offsetHour);
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

    public sealed class HourlyTime
    {
        // Constructors
        public HourlyTime(int minute)
        {
            // Require
            if (0 > minute || minute > 59) throw new ArgumentException();

            // Arguments
            this.Minute = minute;
        }


        // Properties
        public int Minute { get; private set; }


        // Methods
        internal DateTime? Next(DateTime lastExecuteTime, int offsetHour)
        {
            // Next
            var nextExecuteTime = new DateTime(lastExecuteTime.Year, lastExecuteTime.Month, lastExecuteTime.Day, lastExecuteTime.Hour, 0, 0);
            nextExecuteTime = nextExecuteTime.AddHours(offsetHour);
            nextExecuteTime = nextExecuteTime.AddMinutes(this.Minute);

            // Return
            return nextExecuteTime;
        }
    }
}
