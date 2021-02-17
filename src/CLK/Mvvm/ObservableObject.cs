using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Mvvm
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        // Methods
        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            // Require
            if (object.Equals(storage, value)) return;

            // Set
            storage = value;

            // Notify
            this.OnPropertyChanged(propertyName);
        }


        // Events  
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            #region Contracts

            if (string.IsNullOrEmpty(propertyName) == true) throw new ArgumentNullException();

            #endregion

            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
