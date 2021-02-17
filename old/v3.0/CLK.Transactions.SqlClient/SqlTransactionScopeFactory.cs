using CLK.Data.SqlClient;
using CLK.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions.SqlClient
{
    public sealed class SqlTransactionScopeFactory : TransactionScopeFactory
    {
        // Constructors
        public SqlTransactionScopeFactory()
        {

        }

        public void Dispose()
        {

        }


        // Methods
        public TransactionScopeProvider Create()
        {            
            // TransactionScope
            var transactionScope = new SqlTransactionScope();

            // TransactionScopeProvider
            var transactionScopeProvider = new SqlTransactionScopeProvider(transactionScope);

            // Return
            return transactionScopeProvider;
        }
    }
}
