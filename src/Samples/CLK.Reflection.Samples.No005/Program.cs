using CLK.Configuration;
using CLK.Reflection;
using System;

namespace CLK.Reflection.Samples.No005
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            ReflectContext reflectContext = Program.Create();

            // CreateEntity
            SqlRepository sqlRepository = reflectContext.CreateEntity<SqlRepository>("SqlRepositoryGroup");

            // Print
            Console.WriteLine("\nTestEntity.ConnectionString");
            Console.WriteLine(sqlRepository.ConnectionString);

            // End
            Console.WriteLine("\nPress enter to end...");
            Console.ReadLine();
        }

        static ReflectContext Create()
        {
            // ReflectContext
            ReflectContext reflectContext = new ConfigReflectContext();

            // Return
            return reflectContext;
        }
    }
}
