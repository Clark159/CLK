using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Settings
{
    public partial class SettingContext
    {
        // Locator
        private static SettingContext _instance = null;

        public static SettingContext Current
        {
            set
            {
                _instance = value;
            }
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException();
                }
                return _instance;
            }
        }
    }

    public partial class SettingContext
    {
        // Constructors       
        public SettingContext(ISettingRepository appSettingRepository, ISettingRepository connectionStringRepository)
        {
            #region Contracts

            if (appSettingRepository == null) throw new ArgumentNullException();
            if (connectionStringRepository == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            this.AppSettings = new SettingDictionary(appSettingRepository);
            this.ConnectionStrings = new SettingDictionary(connectionStringRepository);
        }


        // Properties
        public SettingDictionary AppSettings { get; private set; }

        public SettingDictionary ConnectionStrings { get; private set; }        
    }
}
