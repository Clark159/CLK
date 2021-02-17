using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CLK.Data.SqlClient
{
    public sealed class SqlCommandScope : IDisposable
    {
        // Fields
        private readonly SqlConnection _connection = null;

        private readonly SqlCommand _command = null;


        // Constructors
        public SqlCommandScope(string connectionString)
        {
            #region Contracts

            if (string.IsNullOrEmpty(connectionString) == true) throw new ArgumentNullException();

            #endregion

            // Transaction Command
            SqlCommand command = SqlTransactionScope.Create(connectionString);
            if (command != null)
            {
                // Connection
                _connection = null;
                
                // Command
                _command = command;

                // Return
                return;
            }

            // Normal Command
            if (command == null)
            {
                // Connection
                _connection = new SqlConnection(connectionString);
                _connection.Open();

                // Command
                _command = new SqlCommand();
                _command.Connection = _connection;

                // Return
                return;
            }
        }

        public void Dispose()
        {
            // Exception
            Exception exception = null;

            // Command
            if (_command != null)
            {
                try
                {
                    _command.Dispose();
                }
                catch (Exception ex)
                {
                    if (exception == null) exception = ex;
                }
            }

            // Connection
            if (_connection != null)
            {
                try
                {
                    _connection.Dispose();
                }
                catch (Exception ex)
                {
                    if (exception == null) exception = ex;
                }
            }

            // Throw
            if (exception != null) throw exception;
        }


        // Properties
        public SqlParameterCollection Parameters
        {
            get { return _command.Parameters; }
        }

        public string CommandText
        {
            get { return _command.CommandText; }
            set { _command.CommandText = value; }
        }


        // Methods
        public int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            return _command.ExecuteScalar();
        }

        public SqlDataReader ExecuteReader()
        {
            return _command.ExecuteReader();
        }

        public XmlReader ExecuteXmlReader()
        {
            return _command.ExecuteXmlReader();
        }
    }
}
