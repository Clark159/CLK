using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class MonthlyTrigger : ITaskTrigger
    {
        // Fields
        private readonly IEnumerable<MonthlyTime> _monthlyTimeCollection = null;


        // Constructors
        public MonthlyTrigger(MonthlyTime monthlyTime)
        {
            #region Contracts

            if (monthlyTime == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _monthlyTimeCollection = new MonthlyTime[] { monthlyTime };
        }

        public MonthlyTrigger(IEnumerable<MonthlyTime> monthlyTimeCollection)
        {
            #region Contracts

            if (monthlyTimeCollection == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _monthlyTimeCollection = monthlyTimeCollection;
        }


        // Methods
        public bool Approve(DateTime executeTime, DateTime lastExecuteTime)
        {
            // Approve
            for (int offsetMonth = 0; offsetMonth <= 1; offsetMonth++)
            {
                foreach (var monthlyTime in _monthlyTimeCollection)
                {
                    // Next
                    var nextExecuteTime = monthlyTime.Next(lastExecuteTime, offsetMonth);
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

    public sealed class MonthlyTime
    {
        // Constructors
        public MonthlyTime(int day)
        {
            // Require
            if (0 > day || day > 31) throw new ArgumentException();

            // Arguments
            this.Day = day;
        }


        // Properties
        public int Day { get; private set; }


        // Methods
        internal DateTime? Next(DateTime lastExecuteTime, int offsetMonth)
        {
            // Next
            var nextExecuteTime = new DateTime(lastExecuteTime.Year, lastExecuteTime.Month, 1, 0, 0, 0);
            nextExecuteTime = nextExecuteTime.AddMonths(offsetMonth);

            if (this.Day > DateTime.DaysInMonth(nextExecuteTime.Year, nextExecuteTime.Month)) return null;
            nextExecuteTime = nextExecuteTime.AddDays(this.Day - 1);

            // Return
            return nextExecuteTime;
        }
    }
}
