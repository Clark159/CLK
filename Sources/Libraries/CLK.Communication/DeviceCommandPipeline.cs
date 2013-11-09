﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLK.Communication
{
    public abstract class DeviceCommandPipeline
    {
        // Methods
        internal void Post(DeviceCommandTask commandTask)
        {
            #region Contracts

            if (commandTask == null) throw new ArgumentException();

            #endregion

            // Events
            commandTask.ExecuteCommandEnded += this.CommandTask_ExecuteCommandEnded;

            // Attach
            this.Attach(commandTask);
        }        
        
        protected abstract void Attach(DeviceCommandTask commandTask);

        protected abstract void Detach(DeviceCommandTask commandTask);

        protected void Execute(DeviceCommandTask commandTask)
        {
            #region Contracts

            if (commandTask == null) throw new ArgumentException();

            #endregion

            WaitCallback executeDelegate = delegate(object state)
            {
                try
                {
                    commandTask.ExecuteCommandAsync();
                }
                catch (Exception ex)
                {
                    Debug.Fail(string.Format("Action:{0}, State:{1}, Message:{2}", "ExecuteCommandAsync", "Exception", ex.Message));
                }
            };
            ThreadPool.QueueUserWorkItem(executeDelegate);
        }


        // Handlers
        private void CommandTask_ExecuteCommandEnded(DeviceCommandTask commandTask)
        {
            #region Contracts

            if (commandTask == null) throw new ArgumentException();

            #endregion

            // Events
            commandTask.ExecuteCommandEnded -= this.CommandTask_ExecuteCommandEnded;

            // Detach
            this.Detach(commandTask);
        }
    }
}
