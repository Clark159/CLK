using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Collections;

namespace CLK.Configuration
{
    public sealed class FreeAttributeDictionary: StoreDictionary<string, string>
    {
        // Constructors
        internal FreeAttributeDictionary(FreeAttributeProvider provider) : base(provider) { }
    }
}