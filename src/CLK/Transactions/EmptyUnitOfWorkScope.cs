using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public sealed class EmptyUnitOfWorkScope : IUnitOfWorkScope
    {
        // Constructors
        public EmptyUnitOfWorkScope()
        {

        }

        public void Dispose()
        {

        }


        // Methods    
        public void Complete()
        {

        }
    }
}
