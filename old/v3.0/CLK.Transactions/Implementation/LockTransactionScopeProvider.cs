using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CLK.Transactions
{
    internal sealed class LockTransactionScopeProvider : TransactionScopeProvider
    {
        // Fields
        private readonly object _syncRoot = null;

        private readonly TransactionScopeProvider _transactionScopeProvider = null;


        // Constructors
        public LockTransactionScopeProvider(object syncRoot, TransactionScopeFactory transactionScopeFactory)
        {
            #region Contracts

            if (syncRoot == null) throw new ArgumentNullException();
            if (transactionScopeFactory == null) throw new ArgumentNullException();

            #endregion

            // Default
            _syncRoot = syncRoot;

            // Execute
            try
            {
                // Monitor
                Monitor.Enter(_syncRoot);

                // TransactionScopeProvider
                _transactionScopeProvider = transactionScopeFactory.Create();
                if (_transactionScopeProvider == null) throw new InvalidOperationException("_transactionScopeProvider=null");
            }
            catch
            {
                // Dispose
                this.Dispose();

                // Throw
                throw;
            }
        }

        public void Dispose()
        {
            // Execute
            try
            {
                // Dispose
                _transactionScopeProvider.Dispose();
            }
            finally
            {
                // Monitor
                Monitor.Exit(_syncRoot);
            }
        }


        // Methods    
        public void Complete()
        {
            // Complete
            _transactionScopeProvider.Complete();
        }
    }
}
