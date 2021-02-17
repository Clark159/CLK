using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Settings;

namespace CLK.Settings.Samples.No003
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            SettingContext settingContext = Program.Create();
            
            // List
            Console.WriteLine("\nAppSettings");
            foreach (string key in settingContext.AppSettings.Keys)
            {
                string argumentString = settingContext.AppSettings[key];
                Console.WriteLine(argumentString);
            }     

            Console.WriteLine("\nConnectionStrings");
            foreach (string key in settingContext.ConnectionStrings.Keys)
            {
                string connectionString = settingContext.ConnectionStrings[key];
                Console.WriteLine(connectionString);
            }                                   

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
