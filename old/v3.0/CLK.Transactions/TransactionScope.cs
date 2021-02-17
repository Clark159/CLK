using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public partial class TransactionScope : TransactionScopeProvider
    {
        // Fields
        private readonly TransactionScopeProvider _transactionScopeProvider = null;


        // Constructors
        internal TransactionScope(TransactionScopeProvider transactionScopeProvider)
        {
            #region Contracts

            if (transactionScopeProvider == null) throw new ArgumentNullException();

            #endregion

            // Default
            _transactionScopeProvider = transactionScopeProvider;
        }

        public void Dispose()
        {
            // Dispose
            _transactionScopeProvider.Dispose();
        }


        // Methods    
        public void Complete()
        {
            // Complete
            _transactionScopeProvider.Complete();
        }
    }

    public partial class TransactionScope : TransactionScopeProvider
    {
        // Constructors
        public TransactionScope() : this(TransactionContext.Current.Create()) { }
    }
}
