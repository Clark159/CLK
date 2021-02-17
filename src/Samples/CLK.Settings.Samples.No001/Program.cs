using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLK.Settings;

namespace CLK.Settings.Samples.No001
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            SettingContext settingContext = Program.Create();

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
