using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Logging
{
    public partial class Logger : LoggerProvider
    {
        // Fields
        private readonly LoggerProvider _loggerProvider = null;


        // Constructors
        internal Logger(LoggerProvider loggerProvider)
        {
            #region Contracts

            if (loggerProvider == null) throw new ArgumentException();

            #endregion

            // Default
            _loggerProvider = loggerProvider;
        }


        // Methods
        public void Debug(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Exception
            while (exception?.InnerException != null)
            {
                exception = exception.InnerException;
            }

            // Log
            _loggerProvider.Debug(message, exception, methodName);
        }

        public void Info(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Exception
            while (exception?.InnerException != null)
            {
                exception = exception.InnerException;
            }

            // Log
            _loggerProvider.Info(message, exception, methodName);
        }

        public void Warn(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Exception
            while (exception?.InnerException != null)
            {
                exception = exception.InnerException;
            }

            // Log
            _loggerProvider.Warn(message, exception, methodName);
        }

        public void Error(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Exception
            while (exception?.InnerException != null)
            {
                exception = exception.InnerException;
            }

            // Log
            _loggerProvider.Error(message, exception, methodName);
        }

        public void Fatal(string message, Exception exception = null, [CallerMemberName]string methodName = "")
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Exception
            while (exception?.InnerException != null)
            {
                exception = exception.InnerException;
            }

            // Log
            _loggerProvider.Fatal(message, exception, methodName);
        }
    }

    public partial class Logger<TCategory> : Logger
    {
        // Constructors
        public Logger() : base(LoggerContext.Current.Create<TCategory>()) { }
    }
}
