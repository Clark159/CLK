using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Settings;

namespace CLK.Settings.Samples.No002
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            SettingContext settingContext = Program.Create();

            // Get
            Console.WriteLine("\nAppSettings");
            string argumentString = settingContext.AppSettings["Argument01"];
            Console.WriteLine(argumentString);

            Console.WriteLine("\nConnectionStrings");
            string connectionString = settingContext.ConnectionStrings["Database01"];
            Console.WriteLine(connectionString);
            
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
