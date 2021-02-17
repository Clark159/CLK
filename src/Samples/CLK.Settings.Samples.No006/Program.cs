using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Settings.Samples.No006
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
            // AppSettingRepository
            MemorySettingRepository appSettingRepository = new MemorySettingRepository();
            appSettingRepository.Add("Argument05", "XXXXXXXXXXXXX");
            appSettingRepository.Add("Argument06", "YYYYYYYYYYYYY");
            appSettingRepository.Add("Argument07", "ZZZZZZZZZZZZZ");

            // ConnectionStringRepository
            MemorySettingRepository connectionStringRepository = new MemorySettingRepository();
            connectionStringRepository.Add("Database04", "Data Source=192.168.4.4;Initial Catalog=DB04;");
            connectionStringRepository.Add("Database05", "Data Source=192.168.5.5;Initial Catalog=DB05;");  

            // SettingContext
            SettingContext settingContext = new MemorySettingContext(appSettingRepository, connectionStringRepository);

            // Return
            return settingContext;
        }
    }
}
