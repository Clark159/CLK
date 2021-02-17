using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class ConcatTrigger : ITaskTrigger
    {
        // Fields 
        private readonly ITaskTrigger _firstTrigger = null;

        private readonly ITaskTrigger _secondTrigger = null;


        // Constructors
        public ConcatTrigger(ITaskTrigger firstTrigger, ITaskTrigger secondTrigger)
        {
            #region Contracts

            if (firstTrigger == null) throw new ArgumentNullException();
            if (secondTrigger == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _firstTrigger = firstTrigger;
            _secondTrigger = secondTrigger;
        }


        // Methods
        public bool Approve(DateTime executeTime, DateTime lastExecuteTime)
        {
            // FirstTrigger
            if (_firstTrigger.Approve(executeTime, lastExecuteTime) == false) return false;

            // SecondTrigger
            if (_secondTrigger.Approve(executeTime, lastExecuteTime) == false) return false;

            // Return
            return true;
        }
    }

    public static class ConcatTriggerExtension
    {
        // Concat
        public static ITaskTrigger Concat(this ITaskTrigger firstTrigger, ITaskTrigger secondTrigger)
        {
            #region Contracts

            if (firstTrigger == null) throw new ArgumentNullException();
            if (secondTrigger == null) throw new ArgumentNullException();

            #endregion

            // Return  
            return new ConcatTrigger(firstTrigger, secondTrigger);
        }
    }
}
