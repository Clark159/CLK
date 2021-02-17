using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLK.Activities
{
    public class NotFoundViewModel : ViewModel
    {
        // Constructors
        public NotFoundViewModel(string message)
        {
            #region Contracts

            if (string.IsNullOrEmpty(message) == true) throw new ArgumentException();

            #endregion

            // Default
            this.Message = message;
        }


        // Properties
        public string Message { get; set; }
    }
}
