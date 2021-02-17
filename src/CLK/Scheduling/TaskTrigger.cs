using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public interface ITaskTrigger
    {
        // Methods
        bool Approve(DateTime executeTime, DateTime lastExecuteTime);
    }
}
