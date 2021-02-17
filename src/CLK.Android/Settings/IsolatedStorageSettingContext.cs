using CLK.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Settings
{
    public sealed class IsolatedStorageSettingContext : SettingContext
    {
        // Constructors
        public IsolatedStorageSettingContext(string configFilename = "App.config") : this(new IsolatedStorageSettingRepository(configFilename, "appSettings"), new IsolatedStorageSettingRepository(configFilename, "connectionStrings")) { }

        public IsolatedStorageSettingContext(ISettingRepository appSettingRepository, ISettingRepository connectionStringRepository) : base(appSettingRepository, connectionStringRepository) { }
    }
}
