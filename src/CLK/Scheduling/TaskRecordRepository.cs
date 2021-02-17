using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public interface ITaskRecordRepository
    {
        // Methods
        void Add(TaskRecord taskRecord);
    }
}
