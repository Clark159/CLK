using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CLK.Transactions
{
    public sealed class DistributedUnitOfWorkScopeProvider : IUnitOfWorkScopeProvider
    {
        // Methods
        public IUnitOfWorkScope Create()
        {            
            // TransactionScope
            TransactionScope transactionScope = new TransactionScope();

            // UnitOfWorkScope
            IUnitOfWorkScope unitOfWorkScope = new DistributedUnitOfWorkScope(transactionScope);

            // Return
            return unitOfWorkScope;
        }
    }
}
