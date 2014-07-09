﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Scheduling
{
    public partial class ScheduleContext
    {
        // Locator
        private static ScheduleContext _current;

        public static ScheduleContext Current
        {
            get
            {
                // Require
                if (_current == null) throw new InvalidOperationException();

                // Return
                return _current;
            }
        }

        public static void Initialize(ScheduleContext context)
        {
            #region Contracts

            if (context == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _current = context;
        }
    }

    public partial class ScheduleContext
    {
        // Fields
        private readonly object _syncRoot = new object();

        private readonly ITaskSettingRepository _taskSettingRepository = null;

        private readonly ITaskStateRepository _taskStateRepository = null;

        private readonly ITaskRecordRepository _taskRecordRepository = null;


        // Constructors
        public ScheduleContext(ITaskSettingRepository taskSettingRepository, ITaskStateRepository taskStateRepository, ITaskRecordRepository taskRecordRepository)
        {
            #region Contracts

            if (taskSettingRepository == null) throw new ArgumentNullException();
            if (taskStateRepository == null) throw new ArgumentNullException();
            if (taskRecordRepository == null) throw new ArgumentNullException();

            #endregion

            // Arguments                   
            _taskSettingRepository = taskSettingRepository;
            _taskStateRepository = taskStateRepository;
            _taskRecordRepository = taskRecordRepository;
        }


        // Methods
        public void Execute(DateTime executeTime)
        {
            lock (_syncRoot)
            {
                // TaskSettingCollection
                var taskSettingCollection = _taskSettingRepository.GetAll();
                if (taskSettingCollection == null) throw new InvalidOperationException();

                // Execute
                foreach (var taskSetting in taskSettingCollection)
                {
                    try
                    {
                        // State
                        var taskState = _taskStateRepository.Get(taskSetting.TaskSettingId);
                        if (taskState == null) taskState = new TaskState(taskSetting.TaskSettingId);

                        // Verify
                        if (taskSetting.TaskTrigger.Verify(executeTime, taskState.LastExecuteTime) == false) continue;

                        // Execute
                        taskSetting.TaskAction.Execute(executeTime);

                        // Record
                        _taskRecordRepository.Add(new TaskRecord(taskSetting.TaskSettingId, executeTime));

                        // Update
                        taskState.LastExecuteTime = executeTime;
                        _taskStateRepository.Set(taskState);
                    }
                    catch (Exception error)
                    {
                        try
                        {
                            // State
                            var taskState = _taskStateRepository.Get(taskSetting.TaskSettingId);
                            if (taskState == null) taskState = new TaskState(taskSetting.TaskSettingId);

                            // Record
                            _taskRecordRepository.Add(new TaskRecord(taskSetting.TaskSettingId, executeTime, error));

                            // Update
                            taskState.LastExecuteTime = executeTime;
                            _taskStateRepository.Set(taskState);
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
