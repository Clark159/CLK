using CLK.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLK.Activities
{
    public abstract class ViewModel : BinableBase, IDisposable
    {
        // Fields
        private SyncContext _syncContext = null;


        // Constructors
        public ViewModel()
        {
            
        }

        public virtual void Start()
        {
           
        }

        public virtual void Dispose()
        {
            
        }


        // Properties
        public SyncContext SyncContext
        {
            get
            {
                // Require
                if (_syncContext == null) throw new InvalidOperationException("_syncContext=null");

                // Return
                return _syncContext;
            }
            internal set
            {
                // Require
                if (value == null) throw new InvalidOperationException("_syncContext=null");

                // Return
                _syncContext = value;
            }
        }
    }
}
