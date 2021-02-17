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
    public partial class ObjectBinding03Form : Form
    {
        // Fields
        private MainViewModel _viewModel = null;


        // Constructors
        public ObjectBinding03Form()
        {
            // Initialize
            this.InitializeComponent();
            this.InitializeBinding();
        }

        private void InitializeBinding()
        {
            // Binding
            var binding = CLK.Mvvm.BindingContext.AddBinding(() => this.ViewModel.Data, () => this.TextBox1.Text, BindingMode.TwoWay);
            this.TextBox1.TextChanged += binding.UpdateToSource;
        }


        // Properties
        public MainViewModel ViewModel
        {
            get
            {
                return _viewModel ?? (_viewModel = new MainViewModel());
            }
        }


        // Handlers
        private void Button1_Click(object sender, EventArgs e)
        {
            this.ViewModel.Update();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.ViewModel.Data);
        }
    }
}
