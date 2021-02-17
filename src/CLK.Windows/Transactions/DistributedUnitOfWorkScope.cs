using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CLK.Transactions
{
    public sealed class DistributedUnitOfWorkScope : IUnitOfWorkScope 
    {
        // Fields
        private readonly TransactionScope _transactionScope = null;


        // Constructors
        public DistributedUnitOfWorkScope(TransactionScope transactionScope)
        {
            #region Contracts

            if (transactionScope == null) throw new ArgumentNullException();

            #endregion

            // Arguments
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
