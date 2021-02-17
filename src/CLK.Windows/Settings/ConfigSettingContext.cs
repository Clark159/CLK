using CLK.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Settings
{
    public sealed class ConfigSettingContext : SettingContext
    {
        // Constructors
        public ConfigSettingContext() : this(ConfigSettingContext.CreateConfiguration()) { }

        public ConfigSettingContext(string configFilename) : this(ConfigSettingContext.CreateConfiguration(configFilename)) { }

        public ConfigSettingContext(System.Configuration.Configuration configuration) : this(new ConfigAppSettingRepository(configuration), new ConfigConnectionStringRepository(configuration)) { }

        public ConfigSettingContext(ISettingRepository appSettingRepository, ISettingRepository connectionStringRepository) : base(appSettingRepository, connectionStringRepository) { }
    

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
