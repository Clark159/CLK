using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Mvvm.Samples.WinForm
{
    public class MainViewModel : ObservableObject
    {
        // Fields
        private int _count = 0;

        private string _data = "Current Data";
        

        // Properties
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                this.SetProperty(ref _data, value);
            }
        }


        // Methods
        public void Update()
        {
            // Update
            _count++;

            // Set
            this.Data = "Current Data : " + _count.ToString();
        }
    }
}
