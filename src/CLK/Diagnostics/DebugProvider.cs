using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Diagnostics
{
    public interface IDebugProvider
    {
        // Methods  
        void Fail(string message);


        void Assert(bool condition);

        void Assert(bool condition, string message);


        void WriteLine(object value);

        void WriteLine(string message);

        void WriteLine(string format, params object[] args);
    }
}
