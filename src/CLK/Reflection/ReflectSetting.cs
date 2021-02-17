using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Reflection
{
    public sealed class ReflectSetting
    {
        // Constructors
        public ReflectSetting(string builderType)
        {
            #region Contracts

            if (string.IsNullOrEmpty(builderType) == true) throw new ArgumentNullException();

            #endregion

            // Arguments
            this.BuilderType = builderType;

            // Default            
            this.Parameters = new Dictionary<string, string>();
        }


        // Properties
        public string BuilderType { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }       
    }
}
