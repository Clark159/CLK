using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLK.Mvvm
{
    public class RelayCommand : ICommand
    {
        // Fields
        private readonly Action _executeDelegate = null;

        private readonly Func<bool> _canExecuteDelegate = null;


        // Constructors
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            #region Contracts

            if (execute == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _executeDelegate = execute;
            _canExecuteDelegate = canExecute;
        }


        // Methods
        public virtual void Execute(object parameter)
        {
            // Require
            if (this.CanExecute(parameter) == false)
            {
                throw new InvalidOperationException();
            }

            // Execute
            _executeDelegate();
        }

        public virtual bool CanExecute(object parameter)
        {
            // Require
            if (_canExecuteDelegate == null)
            {
                return true;
            }

            // Execute
            return _canExecuteDelegate();
        }


        // Events
        public event EventHandler CanExecuteChanged;
        protected void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }

    public class RelayCommand<T> : ICommand
    {
        // Fields
        private readonly Action<T> _executeDelegate = null;

        private readonly Func<T, bool> _canExecuteDelegate = null;


        // Constructors
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            #region Contracts

            if (execute == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _executeDelegate = execute;
            _canExecuteDelegate = canExecute;
        }


        // Methods
        public virtual void Execute(object parameter)
        {
            // Require
            if (this.CanExecute(parameter) == false) 
            {
                throw new InvalidOperationException();
            }

            // Execute
            if (parameter == null)
            {
                _executeDelegate(default(T));
            }
            else
            {
                _executeDelegate((T)parameter);
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            // Require
            if (_canExecuteDelegate == null)
            {
                return true;
            }

            // Execute
            if (parameter == null)
            {
                return _canExecuteDelegate(default(T));
            }
            else
            {
                return _canExecuteDelegate((T)parameter);
            }
        }


        // Events
        public event EventHandler CanExecuteChanged;
        protected void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
