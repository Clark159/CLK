using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CLK.Activities
{
    public abstract class BinableBase : INotifyPropertyChanged
    {
        // Methods        
        protected void SetValue<T>(ref T referenceValue, T setValue, [CallerMemberName]string propertyName = "")
        {
            #region Contracts

            //if (referenceValue == null) throw new ArgumentException();
            //if (setValue == null) throw new ArgumentException();

            #endregion

            // Require
            if (string.IsNullOrEmpty(propertyName) == true) throw new InvalidOperationException();
            if (referenceValue == null && setValue == null) return;
            if (referenceValue!= null && referenceValue.Equals(setValue) == true) return;

            // SetValue
            referenceValue = setValue;

            // Notify
            this.OnPropertyChanged(propertyName);
        }

        protected void SetValue<T>(T originalValue, T setValue, Action setter, [CallerMemberName]string propertyName = "")
        {
            #region Contracts

            //if (originalValue == null) throw new ArgumentException();
            //if (setValue == null) throw new ArgumentException();
            //if (setter == null) throw new ArgumentException();

            #endregion

            // Require
            if (string.IsNullOrEmpty(propertyName) == true) throw new InvalidOperationException();
            if (originalValue == null && setValue == null) return;
            if (originalValue != null && originalValue.Equals(setValue) == true) return;

            // SetValue
            setter?.Invoke();

            // Notify
            this.OnPropertyChanged(propertyName);
        }
        

        // Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            // Require
            if (string.IsNullOrEmpty(propertyName) == true) throw new InvalidOperationException();

            // Raise
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
