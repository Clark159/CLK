using CLK.Configuration;
using CLK.Reflection;
using System;

namespace CLK.Reflection.Samples.No002
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            ReflectContext reflectContext = Program.Create();

            // CreateEntity
            TestEntity testEntity = reflectContext.CreateEntity<TestEntity>("TestEntityGroup");            

            // Print
            Console.WriteLine("\nTestEntity.Print()");
            testEntity.Print();

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
