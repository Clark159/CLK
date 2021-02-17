using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Settings;

namespace CLK.Reflection
{
    public sealed class ConfigReflectContext : ReflectContext
    {
        // Constructors
        public ConfigReflectContext() : this(ConfigReflectContext.CreateConfiguration()) { }

        public ConfigReflectContext(string configFilename) : this(ConfigReflectContext.CreateConfiguration(configFilename)) { }

        public ConfigReflectContext(System.Configuration.Configuration configuration) : this(new ConfigReflectRepository(configuration), new ConfigSettingContext(configuration)) { }

        public ConfigReflectContext(IReflectRepository reflectRepository, SettingContext settingContext) : base(reflectRepository, settingContext) { }


        // Methods 
        private static System.Configuration.Configuration CreateConfiguration()
        {
            // Configuration
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (configuration == null) throw new InvalidOperationException();

            // Return
            return configuration;
        }

        private static System.Configuration.Configuration CreateConfiguration(string configFilename)
        {
            #region Contracts

            if (string.IsNullOrEmpty(configFilename) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (System.IO.File.Exists(configFilename) == false) throw new System.IO.FileNotFoundException(string.Format("Filename:{0}", configFilename));

            // ConfigurationFileMap
            ExeConfigurationFileMap configurationFileMap = new ExeConfigurationFileMap();
            configurationFileMap.ExeConfigFilename = configFilename;

            // Configuration
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            if (configuration == null) throw new InvalidOperationException();

            // Return
            return configuration;
        }
    }
}
