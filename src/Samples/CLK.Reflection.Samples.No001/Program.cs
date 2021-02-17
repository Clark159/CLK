using CLK.Configuration;
using CLK.Reflection;
using System;

namespace CLK.Reflection.Samples.No001
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create
            ReflectContext reflectContext = Program.Create();

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
