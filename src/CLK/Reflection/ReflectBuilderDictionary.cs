using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Collections;

namespace CLK.Reflection
{
    public sealed class ReflectBuilderDictionary : StoreDictionary<string, ReflectBuilder>
    {        
        // Constructors
        internal ReflectBuilderDictionary(IReflectBuilderRepository repository) : base(repository) { }
    }
}
