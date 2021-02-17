using System;

namespace CLK.Reflection.Samples
{
    public class TestEntity
    {
        // Fields        
        private readonly string _parameterA = string.Empty;


        // Constructors
        public TestEntity(string parameterA)
        {
            #region Contracts

            if (string.IsNullOrEmpty(parameterA) == true) throw new ArgumentNullException();

            #endregion

            // Arguments
            _parameterA = parameterA;
        }
        

        // Methods
        public void Print()
        {
            // Write
            Console.WriteLine(_parameterA);
        }
    }
}
