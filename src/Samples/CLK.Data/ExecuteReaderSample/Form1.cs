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

namespace ExecuteReaderSample
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


        // Handlers
        private void QueryAllButton_Click(object sender, EventArgs e)
        {
            // Result
            DataTable dataTable = new DataTable();

            // Get             
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open
                connection.Open();

                // Select
                using (SqlCommand command = new SqlCommand())
                {
                    // Connection
                    command.Connection = connection;

                    // CommandText
                    command.CommandText = @"SELECT Id, Name FROM Users";

                    // Create
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            // Display
            this.DisplayGridView.DataSource = dataTable;
        }

        private void QueryPartButton_Click(object sender, EventArgs e)
        {
            // Input
            int index = Convert.ToInt32(this.IndexTextBox.Text);
            int count = Convert.ToInt32(this.CountTextBox.Text);

            // Result
            DataTable dataTable = new DataTable();

            // Get             
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open
                connection.Open();

                // Select
                using (SqlCommand command = new SqlCommand())
                {
                    // Connection
                    command.Connection = connection;

                    // CommandText
                    command.CommandText = @"SELECT Id, Name FROM Users";

                    // Create
                    using (SqlDataReader reader = command.ExecuteReader(index, count, "Id ASC"))
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            // Display
            this.DisplayGridView.DataSource = dataTable;
        }
    }
}