using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Data.SqlClient
{
    internal sealed class SqlConnectionScope
    {
        // Fields
        private readonly IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

        private readonly List<Guid> _registerConsumerIdList = new List<Guid>();

        private readonly List<Guid> _transactConsumerIdList = new List<Guid>();

        private bool _isCompleted = false;

        private bool _isDisposed = false;


        // Constructors
        public SqlConnectionScope(IsolationLevel isolationLevel)
        {
            // Default
            _isolationLevel = isolationLevel;
        }


        // Properties
        public string ConnectionString { get; private set; }

        public SqlConnection Connection { get; private set; }

        public SqlTransaction Transaction { get; private set; }


        // Methods 
        public void Register(Guid consumerId)
        {
            #region Require

            if (consumerId == Guid.Empty) throw new ArgumentNullException();

            #endregion
            
            // Require
            if (_registerConsumerIdList.Contains(consumerId) == true) throw new InvalidOperationException();
            if (_isDisposed == true) throw new InvalidOperationException();
            if (_isCompleted == true) throw new InvalidOperationException();     
            
            // Add
            if (_registerConsumerIdList.Contains(consumerId) == false) _registerConsumerIdList.Add(consumerId);
            if (_transactConsumerIdList.Contains(consumerId) == false) _transactConsumerIdList.Add(consumerId);
        }

        public void Complete(Guid consumerId)
        {
            #region Require

            if (consumerId == Guid.Empty) throw new ArgumentNullException();

            #endregion

            // Require
            if (_registerConsumerIdList.Contains(consumerId) == false) throw new InvalidOperationException();
            if (_isDisposed == true) throw new InvalidOperationException();
            if (_isCompleted == true) throw new InvalidOperationException();
            
            // Remove
            if (_transactConsumerIdList.Contains(consumerId) == true)
            {
                _transactConsumerIdList.Remove(consumerId);
            }
            if (_transactConsumerIdList.Count > 0) return;

            // Commit
            if (this.Transaction != null)
            {
                this.Transaction.Commit();
            }
            
            // IsCompleted
            _isCompleted = true;
        }

        public bool Dispose(Guid consumerId)
        {
            #region Require

            if (consumerId == Guid.Empty) throw new ArgumentNullException();

            #endregion

            // Require
            if (_registerConsumerIdList.Contains(consumerId) == false) throw new InvalidOperationException();
            if (_isDisposed == true) return false;

            // Remove
            if (_registerConsumerIdList.Contains(consumerId) == true)
            {
                _registerConsumerIdList.Remove(consumerId);
            }
            if (_registerConsumerIdList.Count > 0) return false;
            
            // Transaction
            if (this.Transaction != null)
            {
                if (_isCompleted == false)
                {
                    this.Transaction.Rollback();
                }
                this.Transaction.Dispose();
            }

            // Connection
            if (this.Connection != null)
            {
                this.Connection.Dispose();
            }

            // IsDisposed
            _isDisposed = true;

            // Return
            return true;
        }


        public SqlCommand Create(string connectionString)
        {
            #region Contracts

            if (string.IsNullOrEmpty(connectionString) == true) throw new ArgumentNullException();

            #endregion

            // Initialize
            this.Initialize(connectionString);

            // Command
            var command = new SqlCommand();
            command.Connection = this.Connection;
            command.Transaction = this.Transaction;

            // Return
            return command;
        }

        private void Initialize(string connectionString)
        {
            #region Require

            if (string.IsNullOrEmpty(connectionString) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (this.ConnectionString == connectionString) return;
            if (string.IsNullOrEmpty(this.ConnectionString) == false) throw new InvalidOperationException();

            // ConnectionString
            this.ConnectionString = connectionString;

            // Connection
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();

            // Transaction
            this.Transaction = this.Connection.BeginTransaction(_isolationLevel);
        }
    }
}
