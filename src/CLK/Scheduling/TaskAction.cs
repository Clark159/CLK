using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public interface ITaskAction
    {
        // Methods
        void Execute(DateTime executeTime);
    }
}
