using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CLK.Diagnostics
{
    internal sealed class PortableDebugProvider : IDebugProvider
    {
        // Methods       
        public void Fail(string message)
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentNullException();

            #endregion

            // Debug
            Debug.Assert(false, message);
        }


        public void Assert(bool condition)
        {
            // Debug
            Debug.Assert(condition);
        }

        public void Assert(bool condition, string message)
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentNullException();

            #endregion

            // Debug
            Debug.Assert(condition, message);
        }


        public void WriteLine(object value)
        {
            #region Contracts

            if (value == null) throw new ArgumentNullException();

            #endregion

            // Debug
            Debug.WriteLine(value);
        }

        public void WriteLine(string message)
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentNullException();

            #endregion

            // Debug
            Debug.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            #region Contracts

            if (string.IsNullOrEmpty(format) == true) throw new ArgumentNullException();

            #endregion

            // Debug
            Debug.WriteLine(format, args);
        }
    }
}
