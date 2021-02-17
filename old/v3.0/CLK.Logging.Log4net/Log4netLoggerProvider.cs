using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Logging.Log4net
{
    public class Log4netLoggerProvider : LoggerProvider
    {
        // Fields
        private readonly ILog _logger = null;


        // Constructors
        public Log4netLoggerProvider(ILog logger)
        {
            #region Contracts

            if (logger == null) throw new ArgumentException();

            #endregion

            // Default
            _logger = logger;
        }


        // Methods
        public void Debug(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Setting
            log4net.LogicalThreadContext.Properties["method"] = methodName;

            // Log
            _logger.Debug(message, exception);
        }

        public void Info(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Setting
            log4net.LogicalThreadContext.Properties["method"] = methodName;

            // Log
            _logger.Info(message, exception);
        }

        public void Warn(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Setting
            log4net.LogicalThreadContext.Properties["method"] = methodName;

            // Log
            _logger.Warn(message, exception);
        }

        public void Error(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Setting
            log4net.LogicalThreadContext.Properties["method"] = methodName;

            // Log
            _logger.Error(message, exception);
        }

        public void Fatal(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Setting
            log4net.LogicalThreadContext.Properties["method"] = methodName;

            // Log
            _logger.Fatal(message, exception);
        }
    }
}
