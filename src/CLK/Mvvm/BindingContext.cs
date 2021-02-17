using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Mvvm
{
    public static class BindingContext
    {
        // Fields
        private static object _syncRoot = new object();

        private static List<Binding> _bindingList = new List<Binding>();


        // Methods
        public static Binding AddBinding(Expression<Func<object>> source, Expression<Func<object>> target, BindingMode bindingMode = BindingMode.Default, IValueConverter valueConverter = null)
        {
            #region Contracts

            if (source == null) throw new ArgumentNullException();
            if (target == null) throw new ArgumentNullException();

            #endregion

            // Create
            var binding = new Binding(source, target, bindingMode, valueConverter);

            // Attach
            lock (_syncRoot)
            {           
                // Events
                binding.BindingDisposed += Binding_BindingDisposed;

                // Add
                _bindingList.Add(binding);
            }

            // Return
            return binding;
        }

        public static void RemoveBinding(Binding binding)
        {
            #region Contracts

            if (binding == null) throw new ArgumentNullException();

            #endregion

            // Detach
            lock (_syncRoot)
            {
                // Events
                binding.BindingDisposed -= Binding_BindingDisposed;

                // Remove
                _bindingList.Remove(binding);
            }
        }


        // Handlers
        private static void Binding_BindingDisposed(Binding binding)
        {
            #region Contracts

            if (binding == null) throw new ArgumentNullException();

            #endregion

            // Remove
            BindingContext.RemoveBinding(binding);
        }
    }
}
