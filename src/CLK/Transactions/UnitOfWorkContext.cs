using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Transactions
{
    public partial class UnitOfWorkContext
    {
        // Locator
        private static UnitOfWorkContext _current;

        internal static UnitOfWorkContext Current
        {
            get
            {
                // Require
                if (_current == null) throw new InvalidOperationException();

                // Return
                return _current;
            }
        }

        public static void Initialize(UnitOfWorkContext context)
        {
            #region Contracts

            if (context == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _current = context;
        }
    }

    public partial class UnitOfWorkContext
    {
        // Fields
        private readonly IUnitOfWorkScopeProvider _unitOfWorkScopeProvider = null;


        // Constructors
        public UnitOfWorkContext(IUnitOfWorkScopeProvider unitOfWorkScopeProvider)
        {
            #region Contracts

            if (unitOfWorkScopeProvider == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _unitOfWorkScopeProvider = unitOfWorkScopeProvider;
        }


        // Methods
        public IUnitOfWorkScope Create()
        {
            // Return
            return _unitOfWorkScopeProvider.Create();
        }
    }
}
