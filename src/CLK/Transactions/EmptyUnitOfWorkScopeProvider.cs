using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public sealed class EmptyUnitOfWorkScopeProvider : IUnitOfWorkScopeProvider
    {
        // Methods
        public IUnitOfWorkScope Create()
        {
            // UnitOfWorkScope
            IUnitOfWorkScope unitOfWorkScope = new EmptyUnitOfWorkScope();

            // Return
            return unitOfWorkScope;
        }
    }
}
