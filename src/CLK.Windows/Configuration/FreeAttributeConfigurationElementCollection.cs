using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Configuration
{
    public abstract class FreeAttributeConfigurationElementCollection<TElement> : ConfigurationElementCollection
        where TElement : FreeAttributeConfigurationElement, new()
    {
        // Constructors
        public FreeAttributeConfigurationElementCollection() { }


        // Properties
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }


        // Methods  
        protected override ConfigurationElement CreateNewElement()
        {
            // Base
            return new TElement();
        }        


        public void Add(TElement element)
        {
            #region Contracts

            if (element == null) throw new ArgumentNullException();

            #endregion

            // Base
            this.BaseAdd(element);
        }

        public void Remove(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Base
            this.BaseRemove(name);
        }

        public bool Contains(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Base
            if (this.BaseGet(name) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TElement GetByName(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Base
            return (TElement)this.BaseGet(name);
        }
    }
}
