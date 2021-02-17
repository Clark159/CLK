using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public sealed class LockUnitOfWorkScopeProvider : IUnitOfWorkScopeProvider
    {
        // Fields
        private readonly object _syncRoot = null;

        private readonly IUnitOfWorkScopeProvider _unitOfWorkScopeProvider = null;


        // Constructors
        public LockUnitOfWorkScopeProvider(IUnitOfWorkScopeProvider unitOfWorkScopeProvider) : this(new object(), unitOfWorkScopeProvider) { }

        public LockUnitOfWorkScopeProvider(object syncRoot, IUnitOfWorkScopeProvider unitOfWorkScopeProvider)
        {
            #region Contracts
                                    
            if (syncRoot == null) throw new ArgumentNullException();
            if (unitOfWorkScopeProvider == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _syncRoot = syncRoot;
            _unitOfWorkScopeProvider = unitOfWorkScopeProvider;
        }


        // Methods
        public IUnitOfWorkScope Create()
        {
            // UnitOfWorkScope
            IUnitOfWorkScope unitOfWorkScope = new LockUnitOfWorkScope(_syncRoot, _unitOfWorkScopeProvider);

            // Return
            return unitOfWorkScope;
        }
    }
}
