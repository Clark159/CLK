using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Configuration;

namespace CLK.Reflection
{
    public sealed class ReflectConfigurationElementCollection : FreeAttributeConfigurationElementCollection<ReflectConfigurationElement>
    {
        // Methods  
        protected override object GetElementKey(ConfigurationElement element)
        {
            #region Contracts

            if (element == null) throw new ArgumentNullException();

            #endregion

            // Base
            return ((ReflectConfigurationElement)element).EntityName;
        }
    }
}
