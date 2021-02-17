using System;
using System.Windows.Input;

namespace CLK.Activities
{
    public class RelayCommand : ICommand
    {
        // Fields
        private Action _executeAction = null;

        private Predicate<object> _executePredicate = null;


        // Constructors
        public RelayCommand(Action executeAction, Predicate<object> executePredicate = null)
        {
            #region Contracts
            
            if (executeAction == null) throw new ArgumentException();

            #endregion

            // Default
            _executeAction = executeAction;
            _executePredicate = executePredicate;
        }


        // Methods
        public bool CanExecute(object parameter)
        {
            // Require
            if (_executePredicate == null) return true;

            // CanExecute
            if (parameter == null)
            {
                return _executePredicate.Invoke(default(object));
            }
            else
            {
                return _executePredicate.Invoke(parameter);
            }
        }

        public void Execute(object parameter)
        {
            // Execute
            if (parameter == null)
            {
                _executeAction.Invoke();
            }
            else
            {
                _executeAction.Invoke();
            }           
        }


        // Events
        public event EventHandler CanExecuteChanged;
        protected void OnCanExecuteChanged()
        {
            // Raise
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler.Invoke(this, new EventArgs());
            }
        }
    }

    public class RelayCommand<T> : ICommand
    {
        // Fields
        private Action<T> _executeAction = null;

        private Predicate<T> _executePredicate = null;


        // Constructors
        public RelayCommand(Action<T> executeAction, Predicate<T> executePredicate = null)
        {
            #region Contracts

            if (executeAction == null) throw new ArgumentException();

            #endregion

            // Default
            _executeAction = executeAction;
            _executePredicate = executePredicate;
        }


        // Methods
        public bool CanExecute(object parameter)
        {
            // Require
            if (_executePredicate == null) return true;

            // CanExecute
            if (parameter == null)
            {
                return _executePredicate.Invoke(default(T));
            }
            else
            {
                return _executePredicate.Invoke((T)parameter);
            }
        }

        public void Execute(object parameter)
        {
            // Execute
            if (parameter == null)
            {
                _executeAction.Invoke(default(T));
            }
            else if(parameter != null && parameter is T)
            {
                _executeAction.Invoke((T)parameter);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }


        // Events
        public event EventHandler CanExecuteChanged;
        protected void OnCanExecuteChanged()
        {
            // Raise
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
