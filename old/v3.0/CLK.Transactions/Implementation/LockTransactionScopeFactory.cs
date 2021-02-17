using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public sealed class LockTransactionScopeFactory : TransactionScopeFactory
    {
        // Fields
        private readonly object _syncRoot = new object();

        private readonly TransactionScopeFactory _transactionScopeFactory = null;


        // Constructors
        public LockTransactionScopeFactory(TransactionScopeFactory transactionScopeFactory)
        {
            #region Contracts
                                    
            if (transactionScopeFactory == null) throw new ArgumentNullException();

            #endregion

            // Default
            _transactionScopeFactory = transactionScopeFactory;
        }

        public void Dispose()
        {
            // Dispose
            _transactionScopeFactory.Dispose();
        }


        // Methods
        public TransactionScopeProvider Create()
        {
            // Return
            return new LockTransactionScopeProvider(_syncRoot, _transactionScopeFactory);
        }
    }
}
