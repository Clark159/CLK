using System;
using System.Collections.Generic;
using System.Reflection;
using CLK.Logging;
using CLK.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CLK.AspNetCore
{
    internal static class ActionLoggerExtension
    {
        // Methods
        public static void AddActionLogger(this FilterCollection filters)
        {
            #region Contracts

            if (filters == null) throw new ArgumentException();

            #endregion

            // Attach
            filters.Add(new ActionLoggerFilter());
        }


        // Class
        private class ActionLoggerFilter : IActionFilter
        {
            // Fields
            private readonly object _syncRoot = new object();

            private readonly Dictionary<Type, Logger> _loggerDictionary = new Dictionary<Type, Logger>();

            private readonly string _executingTimeKey = "__" + typeof(ActionLoggerFilter).ToString();


            // Methods
            public void OnActionExecuting(ActionExecutingContext context)
            {
                #region Contracts

                if (context == null) throw new ArgumentException();

                #endregion

                // Duration 
                var executingTime = DateTime.Now;
                context.HttpContext.Items.Add(_executingTimeKey, executingTime);

                // Descriptor
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor == null) throw new InvalidOperationException();

                // Logger
                var logger = this.GetLogger(descriptor.ControllerTypeInfo);
                if (logger == null) throw new InvalidOperationException();

                // Attribute
                var skipLogAttribute = descriptor.MethodInfo.GetCustomAttribute<SkipLogAttribute>();

                // Start
                if (skipLogAttribute == null)
                {
                    logger.Debug("Action started", null, descriptor.MethodInfo.Name);
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                #region Contracts

                if (context == null) throw new ArgumentException();

                #endregion

                // Duration 
                var executingTime = context.HttpContext.Items[_executingTimeKey] as DateTime?;
                var executedTime = DateTime.Now;
                var duration = executedTime - executingTime;

                // Descriptor
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor == null) throw new InvalidOperationException();

                // Logger
                var logger = this.GetLogger(descriptor.ControllerTypeInfo);
                if (logger == null) throw new InvalidOperationException();

                // Attribute
                var skipLogAttribute = descriptor.MethodInfo.GetCustomAttribute<SkipLogAttribute>();

                // Exception
                var exception = context.Exception;
                while (exception?.InnerException != null)
                {
                    exception = exception.InnerException;
                }

                // End
                if (exception == null && skipLogAttribute == null)
                {
                    logger.Debug(string.Format("Action ended: duration={0}", duration?.Milliseconds), exception, descriptor.MethodInfo.Name);
                }

                // Error
                if (exception != null)
                {
                    logger.Error(string.Format("Action error: duration={0}", duration?.Milliseconds), exception, descriptor.MethodInfo.Name);
                }
            }
            
            private Logger GetLogger(Type category)
            {
                #region Contracts

                if (category == null) throw new ArgumentException();

                #endregion

                // Sync
                lock (_syncRoot)
                {
                    // Create
                    if (_loggerDictionary.ContainsKey(category) == false)
                    {
                        // LoggerTpye
                        Type loggerTpye = null;
                        loggerTpye = typeof(Logger<>);
                        loggerTpye = loggerTpye.MakeGenericType(category);

                        // Logger
                        var logger = Activator.CreateInstance(loggerTpye) as Logger;
                        if (logger == null) throw new InvalidOperationException("logger=null");

                        // Add
                        _loggerDictionary.Add(category, logger);
                    }

                    // Return
                    return _loggerDictionary[category];
                }
            }
        }
    }
}
