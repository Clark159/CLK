using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLK.Mvvm.Samples.WinForm
{
    public partial class MainForm : Form
    {
        // Constructors
        public MainForm()
        {
            // Initialize
            this.InitializeComponent();
        }


        // Methods
        private void Navigate(Form subForm)
        {
            // Display
            subForm.FormClosed += delegate
            {
                this.Show();
            };
            subForm.StartPosition = this.StartPosition;
            subForm.Show();

            // Hide
            this.Hide();
        }


        // Handlers
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Navigate(new ObjectBinding01Form());
        }                   

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Navigate(new ObjectBinding02Form());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Navigate(new ObjectBinding03Form());
        }
    }
}
