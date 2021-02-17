using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Configuration
{    
    public partial class FreeAttributeConfigurationElement : ConfigurationElement
    {
        // Constructors
        public FreeAttributeConfigurationElement()
        {            
            // FreeAttributes
            this.FreeAttributes = new FreeAttributeDictionary(new FreeAttributeProvider(this, this.Properties, this.GetPropertyValue, this.SetPropertyValue, this.RemovePropertyValue));
        }


        // Properties
        public FreeAttributeDictionary FreeAttributes { get; private set; }


        // Methods
        private string GetPropertyValue(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Base
            return this[name] as string;
        }

        private void SetPropertyValue(string name, string value)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Base
            this[name] = value;
        }

        private void RemovePropertyValue(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Base
            this[name] = null;
        }


        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();
            if (value == null) throw new ArgumentNullException();

            #endregion

            // Base
            if (base.OnDeserializeUnrecognizedAttribute(name, value) == true)
            {
                return true;
            }

            // AddProperty
            if (this.Properties.Contains(name) == false)
            {
                this.Properties.Add(new ConfigurationProperty(name, typeof(string), null));
            }

            // SetPropertyValue
            this.SetPropertyValue(name, value);

            // Return
            return true;
        }
    }
}
