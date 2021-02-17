using CLK.Configuration;
using System.Collections.Generic;
using CLK.Reflection;
using System;

namespace CLK.Reflection.Samples.No004
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            ReflectContext reflectContext = Program.Create();

            // CreateAllEntity
            IEnumerable<TestEntity> testEntityCollection = reflectContext.CreateAllEntity<TestEntity>("TestEntityGroup");            

            // Print
            Console.WriteLine("\nTestEntity.Print()");
            foreach (var testEntity in testEntityCollection)
            {
                testEntity.Print();
            }

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
