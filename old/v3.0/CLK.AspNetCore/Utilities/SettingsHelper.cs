using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CLK.AspNetCore
{
    public static class SettingsHelper
    {
        // Methods
        public static IConfiguration GetAllAppSettings(string configFilename = @"appsettings.*")
        {
            #region Contracts

            if (string.IsNullOrEmpty(configFilename) == true) throw new ArgumentException();

            #endregion

            // ConfigFileList
            var configFileList = FileHelper.GetAllFile(configFilename);
            if (configFileList == null) throw new InvalidOperationException();

            // ConfigurationBuilder
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            foreach (var configFile in configFileList)
            {
                // Require
                if (configFile.Extension.ToLower() != ".json") continue;

                // Add
                configurationBuilder = configurationBuilder.AddJsonFile(configFile.Name, true, true);
            }

            // Configuration
            var configuration = configurationBuilder.Build();
            if (configuration == null) throw new InvalidOperationException();

            // Return
            return configuration;
        }
    }
}
