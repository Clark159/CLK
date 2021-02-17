using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Configuration;

namespace CLK.Reflection
{
    public sealed class ReflectConfigurationElement : FreeAttributeConfigurationElement
    {
        // Constructors
        public ReflectConfigurationElement()
        {
            // Default
            this.EntityName = string.Empty;
            this.BuilderType = string.Empty;
        }


        // Properties
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string EntityName
        {
            get
            {
                return Convert.ToString(base["name"]);
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("type", IsKey = true, IsRequired = false)]
        public string BuilderType
        {
            get
            {
                return Convert.ToString(base["type"]);
            }
            set
            {
                base["type"] = value;
            }
        }
    }
}
