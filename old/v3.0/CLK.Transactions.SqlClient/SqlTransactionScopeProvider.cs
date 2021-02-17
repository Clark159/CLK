using CLK.Data.SqlClient;
using CLK.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions.SqlClient
{
    public sealed class SqlTransactionScopeProvider : TransactionScopeProvider
    {
        // Fields
        private readonly SqlTransactionScope _transactionScope = null;


        // Constructors
        public SqlTransactionScopeProvider(SqlTransactionScope transactionScope)
        {
            #region Contracts

            if (transactionScope == null) throw new ArgumentNullException();

            #endregion

            // Default
            _transactionScope = transactionScope;
        }

        public void Dispose()
        {
            // Dispose
            _transactionScope.Dispose();
        }


        // Methods    
        public void Complete()
        {
            // Complete
            _transactionScope.Complete();
        }
    }
}
