using CLK.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Settings
{
    public sealed class ConfigConnectionStringRepository : ISettingRepository
    {
        // Fields
        private readonly System.Configuration.Configuration _configuration = null;


        // Constructors
        public ConfigConnectionStringRepository(System.Configuration.Configuration configuration)
        {
            #region Contracts

            if (configuration == null) throw new ArgumentNullException();

            #endregion

            // Configuration
            _configuration = configuration;
        }


        // Methods  
        public void Add(string key, string value)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value) == true) throw new ArgumentNullException();

            #endregion

            // Add
            _configuration.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(key, value));

            // Save
            _configuration.Save();
        }

        public void Remove(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // Remove
            _configuration.ConnectionStrings.ConnectionStrings.Remove(key);

            // Save
            _configuration.Save();
        }

        public bool ContainsKey(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // ContainsKey
            var setting = _configuration.ConnectionStrings.ConnectionStrings[key];
            if (setting == null)
            {
                return false;
            }
            return true;
        }

        public string GetValue(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // GetValue
            var setting = _configuration.ConnectionStrings.ConnectionStrings[key];
            if (setting == null) return null;

            // Return
            return setting.ConnectionString;
        }

        public IEnumerable<string> GetAllKey()
        {
            // Result            
            List<string> keyCollection = new List<string>();

            // GetAllKey
            for (int i = 0; i < _configuration.ConnectionStrings.ConnectionStrings.Count; i++)
            {
                // Setting
                var setting = _configuration.ConnectionStrings.ConnectionStrings[i];
                if (setting == null) throw new InvalidOperationException();

                // Add
                keyCollection.Add(setting.Name);
            }

            // Return
            return keyCollection;
        }        
    }
}
