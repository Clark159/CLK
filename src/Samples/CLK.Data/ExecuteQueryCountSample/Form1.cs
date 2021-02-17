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

namespace ExecuteQueryCountSample
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
        private void QueryCountButton_Click(object sender, EventArgs e)
        {            
            // Result
            int count = 0;

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

                    // Execute
                    count = command.ExecuteQueryCount();
                }
            }

            // Display
            this.DisplayTextBox.Text = count.ToString();
        }
    }
}