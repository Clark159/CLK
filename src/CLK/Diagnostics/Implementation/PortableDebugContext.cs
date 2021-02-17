using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Diagnostics
{
    public sealed class PortableDebugContext : DebugContext
    {        
        // Constructors
        public PortableDebugContext() : this(new PortableDebugProvider()) { }

        public PortableDebugContext(IDebugProvider debugProvider) : base(debugProvider) { }
    }
}
