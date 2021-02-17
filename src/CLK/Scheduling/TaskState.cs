using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Scheduling
{
    public sealed class TaskState
    {
        // Constructors
        public TaskState(string taskSettingId)
        {
            // Arguments
            this.TaskSettingId = taskSettingId;
            this.LastExecuteTime = DateTime.MinValue;
        }

        public TaskState(string taskSettingId, DateTime lastExecuteTime)
        {
            // Arguments
            this.TaskSettingId = taskSettingId;
            this.LastExecuteTime = lastExecuteTime;
        }


        // Properties
        public string TaskSettingId { get; private set; }

        public DateTime LastExecuteTime { get; internal set; }
    }
}
