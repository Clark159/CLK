using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Settings
{
    public sealed class MemorySettingRepository : ISettingRepository
    {
        // Fields
        private readonly IDictionary<string, string> _dictionary = new Dictionary<string, string>();


        // Methods  
        public void Add(string key, string value)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value) == true) throw new ArgumentNullException();

            #endregion

            // Add
            _dictionary.Add(key, value);
        }

        public void Remove(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // Remove
            _dictionary.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // ContainsKey
            return _dictionary.ContainsKey(key);
        }

        public string GetValue(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // GetValue
            return _dictionary[key];
        }

        public IEnumerable<string> GetAllKey()
        {
            // GetAllKey
            return _dictionary.Keys.ToArray();
        }        
    }
}
