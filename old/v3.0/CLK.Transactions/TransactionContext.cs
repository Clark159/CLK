using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public partial class TransactionContext : IDisposable
    {
        // Singleton 
        private static TransactionContext _current;

        internal static TransactionContext Current
        {
            get
            {
                // Require
                if (_current == null) throw new InvalidOperationException("_current=null");

                // Return
                return _current;
            }
        }

        public static TransactionContext Initialize(TransactionScopeFactory transactionScopeFactory)
        {
            #region Contracts

            if (transactionScopeFactory == null) throw new ArgumentNullException();

            #endregion

            // Default
            _current = new TransactionContext(transactionScopeFactory);

            // Return
            return _current;
        }
    }

    public partial class TransactionContext : IDisposable
    {
        // Fields
        private readonly TransactionScopeFactory _transactionScopeFactory = null;


        // Constructors
        internal TransactionContext(TransactionScopeFactory transactionScopeFactory)
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
        internal TransactionScopeProvider Create()
        {
            // TransactionScopeProvider
            var transactionScopeProvider = _transactionScopeFactory.Create();
            if (transactionScopeProvider == null) throw new InvalidOperationException("transactionScopeProvider=null");

            // Return
            return transactionScopeProvider;
        }
    }
}
