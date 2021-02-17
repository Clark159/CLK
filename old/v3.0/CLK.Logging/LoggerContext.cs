using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Logging
{
    public partial class LoggerContext : IDisposable
    {
        // Singleton 
        private static LoggerContext _current;

        internal static LoggerContext Current
        {
            get
            {
                // Require
                if (_current == null) throw new InvalidOperationException("_current=null");

                // Return
                return _current;
            }
        }

        public static LoggerContext Initialize(LoggerFactory loggerFactory)
        {
            #region Contracts

            if (loggerFactory == null) throw new ArgumentException();

            #endregion

            // Default
            _current = new LoggerContext(loggerFactory);

            // Return
            return _current;
        }
    }

    public partial class LoggerContext : IDisposable
    {
        // Fields
        private readonly LoggerFactory _loggerFactory = null;


        // Constructors
        internal LoggerContext(LoggerFactory loggerFactory)
        {
            #region Contracts

            if (loggerFactory == null) throw new ArgumentException();

            #endregion

            // Default
            _loggerFactory = loggerFactory;
        }

        public void Dispose()
        {
            // Dispose
            _loggerFactory.Dispose();
        }


        // Methods
        internal LoggerProvider Create<TCategory>()
        {
            // LoggerProvider
            var loggerProvider = _loggerFactory.Create<TCategory>();
            if (loggerProvider == null) throw new InvalidOperationException("loggerProvider=null");

            // Return
            return loggerProvider;
        }
    }
}
