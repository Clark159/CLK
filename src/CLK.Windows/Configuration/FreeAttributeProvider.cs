using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Collections;

namespace CLK.Configuration
{
    internal sealed partial class FreeAttributeProvider : IStoreProvider<string, string>
    {
        // Snapshot
        private static Dictionary<Type, List<string>> _fixedAttributeNameCollectionSnapshot = new Dictionary<Type, List<string>>();

        private IEnumerable<string> CreateFixedAttributeNameCollection(ConfigurationElement element, ConfigurationPropertyCollection propertyCollection)
        {
            #region Contracts

            if (element == null) throw new ArgumentNullException();

            #endregion

            lock (_fixedAttributeNameCollectionSnapshot)
            {
                // Type        
                Type elementType = element.GetType();

                // Snapshot                
                if (_fixedAttributeNameCollectionSnapshot.ContainsKey(elementType) == false)
                {
                    List<string> fixedAttributeNameSnapshot = new List<string>();
                    foreach (ConfigurationProperty property in propertyCollection)
                    {
                        fixedAttributeNameSnapshot.Add(property.Name);
                    }
                    _fixedAttributeNameCollectionSnapshot.Add(elementType, fixedAttributeNameSnapshot);
                }

                // Return
                return _fixedAttributeNameCollectionSnapshot[elementType].ToArray();
            }
        }
    }

    internal sealed partial class FreeAttributeProvider : IStoreProvider<string, string>
    {
        // Fields
        private readonly ConfigurationPropertyCollection _propertyCollection = null;

        private readonly Func<string, string> _getPropertyValueDelegate = null;

        private readonly Action<string, string> _setPropertyValueDelegate = null;

        private readonly Action<string> _removePropertyValueDelegate = null; 

        private readonly IEnumerable<string> _fixedAttributeNameCollection = null;


        // Constructors
        public FreeAttributeProvider(ConfigurationElement element, ConfigurationPropertyCollection propertyCollection, Func<string, string> getPropertyValueDelegate, Action<string, string> setPropertyValueDelegate, Action<string> removePropertyValueDelegate)
        {
            #region Contracts

            if (element == null) throw new ArgumentNullException();
            if (propertyCollection == null) throw new ArgumentNullException();
            if (getPropertyValueDelegate == null) throw new ArgumentNullException();
            if (setPropertyValueDelegate == null) throw new ArgumentNullException();
            if (removePropertyValueDelegate == null) throw new ArgumentNullException();

            #endregion            

            // Arguments
            _propertyCollection = propertyCollection;
            _getPropertyValueDelegate = getPropertyValueDelegate;
            _setPropertyValueDelegate = setPropertyValueDelegate;
            _removePropertyValueDelegate = removePropertyValueDelegate;

            // FixedAttributeNameCollection
            _fixedAttributeNameCollection = this.CreateFixedAttributeNameCollection(element, propertyCollection);
        }


        // Methods
        public void Add(string name, string value)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (_fixedAttributeNameCollection.Contains(name) == true) throw new ArgumentNullException();

            // AddProperty
            if (_propertyCollection.Contains(name) == false)
            {
                _propertyCollection.Add(new ConfigurationProperty(name, typeof(string), null));
            }
            
            // SetPropertyValue
            _setPropertyValueDelegate(name, value);
        }

        public void Remove(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (_fixedAttributeNameCollection.Contains(name) == true) throw new ArgumentNullException();
            if (_propertyCollection.Contains(name) == false) return;

            // RemovePropertyValue
            _removePropertyValueDelegate(name);
        }

        public bool ContainsKey(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (_fixedAttributeNameCollection.Contains(name) == true) return false;     
            if (_propertyCollection.Contains(name) == false) return false;     

            // ContainsKey
            string value = _getPropertyValueDelegate(name);
            if (string.IsNullOrEmpty(value) == false)
            {
                return true;
            }
            return false;
        }

        public string GetValue(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (_fixedAttributeNameCollection.Contains(name) == true) return null;
            if (_propertyCollection.Contains(name) == false) return null;

            // GetPropertyValue
            return _getPropertyValueDelegate(name);
        }

        public IEnumerable<string> GetAllKey()
        {
            // Result
            List<string> freeAttributeNameCollection = new List<string>();

            // Create           
            foreach (ConfigurationProperty property in _propertyCollection)
            {
                if (_fixedAttributeNameCollection.Contains(property.Name) == false)
                {
                    if (string.IsNullOrEmpty(_getPropertyValueDelegate(property.Name)) == false)
                    {
                        freeAttributeNameCollection.Add(property.Name);
                    }                   
                }                
            }

            // return
            return freeAttributeNameCollection;
        }        
    }
}
