using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class EmptyAction : ITaskAction
    {
        // Methods
        public void Execute(DateTime executeTime)
        {
            // Nothing
        }
    }
}
