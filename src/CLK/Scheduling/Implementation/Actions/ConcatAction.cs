using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class ConcatAction : ITaskAction
    {
        // Fields 
        private readonly ITaskAction _firstAction = null;

        private readonly ITaskAction _secondAction = null;


        // Constructors
        public ConcatAction(ITaskAction firstAction, ITaskAction secondAction)
        {
            #region Contracts

            if (firstAction == null) throw new ArgumentNullException();
            if (secondAction == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _firstAction = firstAction;
            _secondAction = secondAction;
        }


        // Methods
        public void Execute(DateTime executeTime)
        {
            // FirstAction
            _firstAction.Execute(executeTime);

            // SecondAction
            _secondAction.Execute(executeTime);
        }
    }

    public static class ConcatActionExtension
    {
        // Concat
        public static ITaskAction Concat(this ITaskAction firstAction, ITaskAction secondAction)
        {
            #region Contracts

            if (firstAction == null) throw new ArgumentNullException();
            if (secondAction == null) throw new ArgumentNullException();

            #endregion

            // Return  
            return new ConcatAction(firstAction, secondAction);
        }
    }
}
