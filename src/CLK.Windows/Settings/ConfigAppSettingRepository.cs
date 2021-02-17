using CLK.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Settings
{
    public sealed class ConfigAppSettingRepository : ISettingRepository
    {
        // Fields
        private readonly System.Configuration.Configuration _configuration = null;


        // Constructors
        public ConfigAppSettingRepository(System.Configuration.Configuration configuration)
        {
            #region Contracts

            if (configuration == null) throw new ArgumentNullException();

            #endregion

            // Arguments
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
            _configuration.AppSettings.Settings.Add(key, value);

            // Save
            _configuration.Save();
        }

        public void Remove(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // Remove
            _configuration.AppSettings.Settings.Remove(key);

            // Save
            _configuration.Save();
        }

        public bool ContainsKey(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // ContainsKey
            var setting = _configuration.AppSettings.Settings[key];
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
            var setting = _configuration.AppSettings.Settings[key];
            if (setting == null) return null;

            // Return
            return setting.Value;
        }

        public IEnumerable<string> GetAllKey()
        {
            // GetAllKey
            return _configuration.AppSettings.Settings.AllKeys.ToArray();
        }        
    }
}
