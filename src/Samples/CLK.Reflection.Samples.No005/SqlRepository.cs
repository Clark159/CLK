using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Reflection.Samples.No005
{
    public class SqlRepository
    {
        // Constructors
        public SqlRepository(string connectionString)
        {
            #region Contracts

            if (string.IsNullOrEmpty(connectionString) == true) throw new ArgumentNullException();

            #endregion

            // Arguments
            this.ConnectionString = connectionString;
        }


        // Properties   
        public string ConnectionString { get; private set; }
    }
}
