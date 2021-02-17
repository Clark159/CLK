using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using CLK.Activities;
using CLK.Autofac;

namespace CLK.Windows
{
    public class WindowContext : IDisposable
    {
        // Fields
        private readonly AutofacContext _autofacContext = null;

        private readonly Window _window = null;


        // Constructors
        public WindowContext(AutofacContext autofacContext, Window window = null)
        {
            #region Contracts

            if (autofacContext == null) throw new ArgumentException();

            #endregion

            // Default
            _autofacContext = autofacContext;

            // Window
            if (window == null)
            {
                window = new Window();
            }
            _window = window;
        }

        public void Start()
        {
            // Require
            if (System.Windows.Application.Current == null) throw new InvalidOperationException("Application.Current=null");

            // Shell
            Shell shell = null;
            if (_autofacContext.Container.IsRegistered<Shell>() == true && _autofacContext.Container.IsRegistered<ShellModel>() == true)
            {
                shell = _autofacContext.Container.Resolve<Shell>();
                shell.DataContext = _autofacContext.Container.Resolve<ShellModel>();
            }

            // Start
            _window.Content = shell;            
            _window.Show();
        }

        public void Dispose()
        {
            // Dispose
            _window.Close();
        }
    }
}
