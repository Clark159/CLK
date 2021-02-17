using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public sealed class DistributedTransactionScopeFactory : TransactionScopeFactory
    {
        // Constructors
        public DistributedTransactionScopeFactory()
        {

        }

        public void Dispose()
        {
           
        }


        // Methods
        public TransactionScopeProvider Create()
        {
            // TransactionScope
            System.Transactions.TransactionScope transactionScope = new System.Transactions.TransactionScope();

            // TransactionScopeProvider
            TransactionScopeProvider transactionScopeProvider = new DistributedTransactionScopeProvider(transactionScope);

            // Return
            return transactionScopeProvider;
        }
    }
}
