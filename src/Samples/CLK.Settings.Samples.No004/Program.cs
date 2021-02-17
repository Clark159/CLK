using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Settings;

namespace CLK.Settings.Samples.No004
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            SettingContext settingContext = Program.Create();

            // Set           
            settingContext.AppSettings.Add("Argument04", "DDDDDDDDDDDDD");

            settingContext.ConnectionStrings.Add("Database03", "Data Source=192.168.3.3;Initial Catalog=DB03");

            // End
            Console.WriteLine("\nPress enter to end...");
            Console.ReadLine();
        }

        static SettingContext Create()
        {
            // SettingContext
            SettingContext settingContext = new ConfigSettingContext();

            // Return
            return settingContext;
        }
    }
}
