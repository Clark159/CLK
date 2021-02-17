using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection.Samples.No006
{
    public class SubEntity
    {
        // Fields        
        private readonly string _parameterA = string.Empty;


        // Constructors
        public SubEntity(string parameterA)
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
