using CLK.Configuration;
using CLK.Reflection;
using System;

namespace CLK.Reflection.Samples.No006
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            ReflectContext reflectContext = Program.Create();

            // CreateEntity
            TopEntity topEntity = reflectContext.CreateEntity<TopEntity>("TopEntityGroup");

            // Print
            Console.WriteLine("\nTopEntity.SubEntity.Print()");
            topEntity.SubEntity.Print();

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
