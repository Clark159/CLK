using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public interface ITaskStateRepository
    {
        // Methods
        void Set(TaskState taskState);

        TaskState Get(string taskSettingId);
    }
}
