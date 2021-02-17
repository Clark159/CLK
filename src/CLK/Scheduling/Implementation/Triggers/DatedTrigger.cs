using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class DatedTrigger : ITaskTrigger
    {
        // Fields
        private readonly IEnumerable<DateTime> _dateCollection = null;


        // Constructors
        public DatedTrigger(DateTime date)
        {
            // DateCollection
            _dateCollection = new DateTime[] { date.Date };
        }

        public DatedTrigger(IEnumerable<DateTime> dateCollection)
        {
            #region Contracts

            if (dateCollection == null) throw new ArgumentNullException();

            #endregion

            // DateCollection
            var dateList = new List<DateTime>();
            foreach (var date in dateCollection)
            {
                dateList.Add(date.Date);
            }
            _dateCollection = dateList;
        }


        // Methods
        public bool Approve(DateTime executeTime, DateTime lastExecuteTime)
        {
            // Date
            var executeDate = executeTime.Date;
            var lastExecuteDate = lastExecuteTime.Date;

            // Approve
            foreach (var nextExecuteDate in _dateCollection)
            {
                // Check
                if (nextExecuteDate > lastExecuteTime)
                {
                    if (nextExecuteDate <= executeTime)
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
