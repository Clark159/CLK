using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CLK.Transactions
{
    public sealed class LockUnitOfWorkScope : IUnitOfWorkScope
    {
        // Fields
        private readonly object _syncRoot = null;

        private readonly IUnitOfWorkScope _unitOfWorkScope = null;
        

        // Constructors
        public LockUnitOfWorkScope(object syncRoot, Func<IUnitOfWorkScope> createUnitOfWorkScopeDelegate)
        {
            #region Contracts

            if (syncRoot == null) throw new ArgumentNullException();
            if (createUnitOfWorkScopeDelegate == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _syncRoot = syncRoot;

            // Enter
            try
            {
                // Monitor
                Monitor.Enter(_syncRoot);

                // UnitOfWorkScope
                _unitOfWorkScope = createUnitOfWorkScopeDelegate();
                if (_unitOfWorkScope == null) throw new InvalidOperationException();
            }
            catch
            {
                // Dispose
                this.Dispose();

                // Throw
                throw;
            }
        }

        public LockUnitOfWorkScope(object syncRoot, IUnitOfWorkScopeProvider unitOfWorkScopeProvider)
        {
            #region Contracts

            if (syncRoot == null) throw new ArgumentNullException();
            if (unitOfWorkScopeProvider == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _syncRoot = syncRoot;

            // Enter
            try
            {
                // Monitor
                Monitor.Enter(_syncRoot);

                // UnitOfWorkScope
                _unitOfWorkScope = unitOfWorkScopeProvider.Create();
                if (_unitOfWorkScope == null) throw new InvalidOperationException();
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
            // Exit
            try
            {
                // UnitOfWorkScope
                if (_unitOfWorkScope != null)
                {
                    _unitOfWorkScope.Dispose();
                }
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
            if (_unitOfWorkScope != null)
            {
                _unitOfWorkScope.Complete();
            }
        }
    }
}
