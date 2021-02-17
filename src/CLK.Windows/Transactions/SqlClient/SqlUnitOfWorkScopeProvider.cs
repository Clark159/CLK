using CLK.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CLK.Transactions.SqlClient
{
    public sealed class SqlUnitOfWorkScopeProvider : IUnitOfWorkScopeProvider
    {
        // Methods
        public IUnitOfWorkScope Create()
        {            
            // TransactionScope
            SqlTransactionScope transactionScope = new SqlTransactionScope();

            // UnitOfWorkScope
            IUnitOfWorkScope unitOfWorkScope = new SqlUnitOfWorkScope(transactionScope);

            // Return
            return unitOfWorkScope;
        }
    }
}
