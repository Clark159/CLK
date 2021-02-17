using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CLK.Diagnostics
{
    public partial class DebugContext
    {
        // Locator
        private static DebugContext _instance = null;

        public static DebugContext Current
        {
            set
            {
                _instance = value;
            }
            get
            {
                if (_instance == null)
                {
                    _instance = new PortableDebugContext();
                }
                return _instance;
            }
        }
    }

    public partial class DebugContext
    {
        // Fields        
        private IDebugProvider _debugProvider = null;


        // Constructors
        public DebugContext(IDebugProvider debugProvider)
        {
            #region Contracts

            if (debugProvider == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _debugProvider = debugProvider;
        }


        // Methods       
        public void Fail(string message)
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentNullException();

            #endregion

            // Provider
            _debugProvider.Fail(message);
        }


        public void Assert(bool condition)
        {
            // Provider
            _debugProvider.Assert(condition);
        }
        
        public void Assert(bool condition, string message)
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentNullException();

            #endregion

            // Provider
            _debugProvider.Assert(condition, message);
        }
                   

        public void WriteLine(object value)
        {
            #region Contracts

            if (value==null) throw new ArgumentNullException();

            #endregion

            // Provider
            _debugProvider.WriteLine(value);
        }

        public void WriteLine(string message)
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentNullException();

            #endregion

            // Provider
            _debugProvider.WriteLine(message);
        }
        
        public void WriteLine(string format, params object[] args)
        {
            #region Contracts

            if (string.IsNullOrEmpty(format) == true) throw new ArgumentNullException();

            #endregion

            // Provider
            _debugProvider.WriteLine(format, args);
        }
    }    
}
