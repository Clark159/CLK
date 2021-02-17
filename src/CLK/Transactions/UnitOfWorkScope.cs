using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Transactions
{
    public sealed class UnitOfWorkScope : IUnitOfWorkScope
    {
        // Fields
        private readonly IUnitOfWorkScope _unitOfWorkScope = null;


        // Constructors
        public UnitOfWorkScope() : this(UnitOfWorkContext.Current.Create()) { }

        internal UnitOfWorkScope(IUnitOfWorkScope unitOfWorkScope)
        {
            #region Contracts

            if (unitOfWorkScope == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _unitOfWorkScope = unitOfWorkScope;
        }

        public void Dispose()
        {
            // Dispose
            _unitOfWorkScope.Dispose();
        }


        // Methods    
        public void Complete()
        {
            // Complete
            _unitOfWorkScope.Complete();
        }        
    }
}
