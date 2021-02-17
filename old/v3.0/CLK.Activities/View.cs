using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CLK.Activities
{
    public partial class View : UserControl, INotifyPropertyChanged
    {
        // Fields
        private ViewModel _model = null;


        // Constructors
        public View()
        {
            // Events
            this.DataContextChanged += (s, e) => { this.Initialize(); };
            this.Loaded += (s, e) => { this.Start(); };
            this.Unloaded += (s, e) => { this.Dispose(); };
        }

        protected virtual void Initialize()
        {
            // Require
            if (this.DataContext == null) throw new InvalidOperationException("this.DataContext=null");
            if (this.DataContext is ViewModel == false) throw new InvalidOperationException("this.DataContext=" + this.DataContext.GetType().FullName);
            if (_model != null) throw new InvalidOperationException("_model!=null");

            // Model
            _model = this.DataContext as ViewModel;
        }

        protected virtual void Start()
        {
            // Require
            if (_model == null) return;

            // Start
            _model.SyncContext = new WpfSyncContext(this.Dispatcher);
            _model.Start();
        }

        protected virtual void Dispose()
        {
            // Require
            if (_model == null) return;

            // Dispose
            _model.Dispose();
        }
    }

    public partial class View : UserControl, INotifyPropertyChanged
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
            if (referenceValue != null && referenceValue.Equals(setValue) == true) return;

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
