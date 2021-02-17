using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Data.SqlClient
{
    public sealed partial class SqlTransactionScope : IDisposable
    {
        // Fields
        [ThreadStatic]
        private static SqlConnectionScope _connectionScope = null;

        private readonly Guid _consumerId = Guid.NewGuid();


        // Constructors
        public SqlTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            // Register
            if (_connectionScope == null)
            {
                _connectionScope = new SqlConnectionScope(isolationLevel);
            }
            _connectionScope.Register(_consumerId);
        }

        public void Dispose()
        {
            // Require
            if (_connectionScope == null) throw new InvalidOperationException();

            // Dispose
            if (_connectionScope.Dispose(_consumerId) == true)
            {
                _connectionScope = null;
            }
        }


        // Methods    
        public void Complete()
        {
            // Require
            if (_connectionScope == null) throw new InvalidOperationException();

            // Complete
            _connectionScope.Complete(_consumerId);
        }
    }

    public sealed partial class SqlTransactionScope
    {
        // Methods   
        internal static SqlCommand Create(string connectionString)
        {
            #region Contracts

            if (string.IsNullOrEmpty(connectionString) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (_connectionScope == null) return null;

            // Create
            return _connectionScope.Create(connectionString);
        }
    }
}
