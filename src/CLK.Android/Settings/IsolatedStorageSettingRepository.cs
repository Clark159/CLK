using CLK.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLK.Settings
{
    public sealed class IsolatedStorageSettingRepository : ISettingRepository
    {
        // Fields
        private readonly string _configFilename = null;

        private readonly string _settingElementName = null;


        // Constructors
        public IsolatedStorageSettingRepository(string configFilename, string settingElementName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(configFilename) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(settingElementName) == true) throw new ArgumentNullException();

            #endregion

            // Arguments
            _configFilename = configFilename;
            _settingElementName = settingElementName;

            // Default                
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // ReadStream
                IsolatedStorageFileStream readStream = null;
                if (storage.FileExists(_configFilename) == false)
                {
                    readStream = storage.CreateFile(_configFilename);
                }
                else
                {
                    readStream = new IsolatedStorageFileStream(_configFilename, FileMode.Open, storage);
                }

                // Document
                XDocument document = null;
                bool isDirty = false;      
                try
                {
                    using (readStream)
                    {
                        document = XDocument.Load(readStream);
                    }
                }
                catch
                {
                    document = new XDocument(new XElement("configuration"));
                    isDirty = true;
                }
                if (document.Root == null) throw new InvalidOperationException();

                // SettingElement
                XElement settingElement = document.Root.Element(_settingElementName);
                if (settingElement == null)
                {
                    settingElement = new XElement(_settingElementName);
                    document.Root.Add(settingElement);
                    isDirty = true;
                }

                // Save
                if (isDirty == true)
                {
                    using (var writeStream = new IsolatedStorageFileStream(_configFilename, FileMode.Create, storage))
                    {
                        document.Save(writeStream);
                    }
                }
            }
        }


        // Methods  
        public void Add(string key, string value)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value) == true) throw new ArgumentNullException();

            #endregion
            
            // Add
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Document
                XDocument document = null;
                using (var readStream = new IsolatedStorageFileStream(_configFilename, FileMode.Open, storage))
                {             
                    document = XDocument.Load(readStream);                                     
                }
                
                // Element
                var element = new XElement("add");
                element.SetAttributeValue("key", key);
                element.SetAttributeValue("value", value);
                document.Root.Element(_settingElementName).Add(element);

                // Save
                using (var writeStream = new IsolatedStorageFileStream(_configFilename, FileMode.Create, storage))
                {
                    document.Save(writeStream);
                }
            }
        }

        public void Remove(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // Remove
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Document
                XDocument document = null;
                bool isDirty = false;    
                using (var readStream = new IsolatedStorageFileStream(_configFilename, FileMode.Open, storage))
                {
                    document = XDocument.Load(readStream);
                }

                // Element
                foreach (var element in document.Root.Element(_settingElementName).Elements().Where(e => e.Attribute("key").Value==key).ToArray())
                {
                    element.Remove();
                    isDirty = true;
                }

                // Save
                if (isDirty == true)
                {
                    using (var writeStream = new IsolatedStorageFileStream(_configFilename, FileMode.Create, storage))
                    {
                        document.Save(writeStream);
                    }
                }
            }
        }

        public bool ContainsKey(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // ContainsKey
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Document
                XDocument document = null;
                using (var readStream = new IsolatedStorageFileStream(_configFilename, FileMode.Open, storage))
                {
                    document = XDocument.Load(readStream);
                }

                // Element
                var element = document.Root.Element(_settingElementName).Elements().Where(e => e.Attribute("key").Value == key).FirstOrDefault();
                if (element == null)
                {
                    return false;
                }

                // Return
                return true;
            }
        }

        public string GetValue(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // GetValue
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Document
                XDocument document = null;
                using (var readStream = new IsolatedStorageFileStream(_configFilename, FileMode.Open, storage))
                {
                    document = XDocument.Load(readStream);
                }

                // Element
                var element = document.Root.Element(_settingElementName).Elements().Where(e => e.Attribute("key").Value == key).FirstOrDefault();
                if (element == null)
                {
                    return null;
                }

                // Attribute
                var attribute = element.Attribute("value");
                if (attribute == null)
                {
                    return null;
                }

                // Return
                return attribute.Value;
            }
        }

        public IEnumerable<string> GetAllKey()
        {
            // Result
            List<string> keyList = new List<string>();

            // GetAllKey            
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Document
                XDocument document = null;
                using (var readStream = new IsolatedStorageFileStream(_configFilename, FileMode.Open, storage))
                {
                    document = XDocument.Load(readStream);
                }

                // Element
                foreach (var element in document.Root.Element(_settingElementName).Elements())
                {
                    // Attribute
                    var attribute = element.Attribute("value");
                    if (attribute == null) continue;
                    keyList.Add(attribute.Value);
                }              
            }

            // Return
            return keyList;
        }
    }
}
