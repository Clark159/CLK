using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Collections;

namespace CLK.Settings
{
    public sealed class SettingDictionary : StoreDictionary<string, string>
    {        
        // Constructors
        internal SettingDictionary(ISettingRepository repository) : base(repository) { }


        // Methods 
        public void AddDefault(string key, string value)
        {
            // Require
            if (this.ContainsKey(key) == true) return;

            // Add
            this.Add(key, value);
        }
    }
}
