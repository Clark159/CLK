using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public interface ITaskSettingRepository
    {
        // Methods
        IEnumerable<TaskSetting> GetAll();
    }
}
