using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Settings;
using System.Runtime;

namespace CLK.Reflection
{
    public abstract class ReflectBuilder
    {
        // Constructors
        public ReflectBuilder()
        {
            // Default            
            this.Parameters = new Dictionary<string, string>();
        }


        // Properties
        internal protected Dictionary<string, string> Parameters { get; private set; }

        internal protected IReflectContext ReflectContext { get; internal set; }

        internal protected SettingContext SettingContext { get; internal set; }  


        // Methods        
        internal protected abstract object CreateEntity();


        protected string GetParameter(string name, string defaultValue = default(string))
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (this.Parameters.ContainsKey(name) == false) return defaultValue;

            // Get
            string valueString = this.Parameters[name];
            if (string.IsNullOrEmpty(valueString) == true) throw new ArgumentNullException();

            // Convert
            string value = valueString;

            // Return
            return value;
        }

        protected TValue GetParameter<TValue>(string name, TValue defaultValue, Func<string, TValue> converter)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();
            if (converter == null) throw new ArgumentNullException();

            #endregion

            // Require
            if (this.Parameters.ContainsKey(name) == false) return defaultValue;

            // Get
            string valueString = this.Parameters[name];
            if (string.IsNullOrEmpty(valueString) == true) throw new ArgumentNullException();

            // Convert
            TValue value = converter(this.Parameters[name]);

            // Return
            return value;
        }


        protected void SetParameter(string name, string value)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value) == true) throw new ArgumentNullException();

            #endregion

            // Convert
            string valueString = value;
            if (string.IsNullOrEmpty(valueString) == true) throw new ArgumentNullException();

            // Set
            if (this.Parameters.ContainsKey(name) == true)
            {
                this.Parameters[name] = valueString;
            }
            else
            {
                this.Parameters.Add(name, valueString);
            }
        }

        protected void SetParameter<TValue>(string name, TValue value, Func<TValue, string> converter)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException();
            if (value == null) throw new ArgumentNullException();
            if (converter == null) throw new ArgumentNullException();

            #endregion

            // Convert
            string valueString = converter(value);
            if (string.IsNullOrEmpty(valueString) == true) throw new ArgumentNullException();

            // Set
            if (this.Parameters.ContainsKey(name) == true)
            {
                this.Parameters[name] = valueString;
            }
            else
            {
                this.Parameters.Add(name, valueString);
            }
        }
    }
}
