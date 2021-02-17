using CLK.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlTransactionScopeSample
{
    public partial class Form1 : Form
    {
        // Fields
        private readonly string _connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\SampleDatabase.mdf;Integrated Security=True";


        // Constructors
        public Form1()
        {
            // Base
            InitializeComponent();
        }


        // Methods
        private void InsertData()
        {
            // Select
            using (var command = new SqlCommandScope(_connectionString))
            {
                // CommandParameters
                command.Parameters.Add(new SqlParameter("@Id", Guid.NewGuid().ToString()));
                command.Parameters.Add(new SqlParameter("@Name", Guid.NewGuid().ToString()));

                // CommandText
                command.CommandText = @"INSERT INTO Users 
                                                    (Id, Name)
                                        VALUES      (@Id, @Name)";

                // Execute
                command.ExecuteNonQuery();
            }
        }

        private void RefreshData()
        {
            // Result
            DataTable dataTable = new DataTable();

            // Select
            using (var command = new SqlCommandScope(_connectionString))
            {
                // CommandText
                command.CommandText = @"SELECT Id, Name FROM Users";

                // Create
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            // Display
            this.DisplayGridView.DataSource = dataTable;
        }        

        
        // Handlers
        private void Insert_NormalButton_Click(object sender, EventArgs e)
        {
            // Insert
            this.InsertData();

            // Refresh
            this.RefreshData();
        }

        private void Insert_TransactionCommitButton_Click(object sender, EventArgs e)
        {
            // Transaction
            using (var transaction = new SqlTransactionScope())
            {
                try
                {
                    // Insert
                    this.InsertData();

                    // Complete
                    transaction.Complete();
                }
                catch
                {
                    // ......
                }
            }

            // Refresh
            this.RefreshData();
        }

        private void Insert_TransactionRollbackButton_Click(object sender, EventArgs e)
        {
            // Transaction
            using (var transaction = new SqlTransactionScope())
            {
                try
                {
                    // Insert
                    this.InsertData();

                    // Throw
                    throw new Exception();

                    // Complete
                    transaction.Complete();
                }
                catch
                {
                    // ......
                }
            }

            // Refresh
            this.RefreshData();
        }
    }
}