using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class YearlyTrigger : ITaskTrigger
    {
        // Fields
        private readonly IEnumerable<YearlyTime> _yearlyTimeCollection = null;


        // Constructors
        public YearlyTrigger(YearlyTime yearlyTime)
        {
            #region Contracts

            if (yearlyTime == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _yearlyTimeCollection = new YearlyTime[] { yearlyTime };
        }

        public YearlyTrigger(IEnumerable<YearlyTime> yearlyTimeCollection)
        {
            #region Contracts

            if (yearlyTimeCollection == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _yearlyTimeCollection = yearlyTimeCollection;
        }


        // Methods
        public bool Approve(DateTime executeTime, DateTime lastExecuteTime)
        {
            // Approve
            for (int offsetYear = 0; offsetYear <= 1; offsetYear++)
            {
                foreach (var yearlyTime in _yearlyTimeCollection)
                {
                    // Next
                    var nextExecuteTime = yearlyTime.Next(lastExecuteTime, offsetYear);
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

    public sealed class YearlyTime
    {
        // Constructors
        public YearlyTime(int month, int day)
        {
            // Require
            if (0 > month || month > 12) throw new ArgumentException();
            if (0 > day || day > 31) throw new ArgumentException();

            // Arguments
            this.Month = month;
            this.Day = day;
        }


        // Properties
        public int Month { get; private set; }

        public int Day { get; private set; }


        // Methods
        internal DateTime? Next(DateTime lastExecuteTime, int offsetYear)
        {
            // Next
            var nextExecuteTime = new DateTime(lastExecuteTime.Year, 1, 1, 0, 0, 0);
            nextExecuteTime = nextExecuteTime.AddYears(offsetYear);
            nextExecuteTime = nextExecuteTime.AddMonths(this.Month - 1);

            if (this.Day > DateTime.DaysInMonth(nextExecuteTime.Year, nextExecuteTime.Month)) return null;
            nextExecuteTime = nextExecuteTime.AddDays(this.Day - 1);

            // Return
            return nextExecuteTime;
        }
    }
}
