using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extensions.DependencyInjection;
using CLK.Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace CLK.AspNetCore
{
    public class LazyAutofacServiceProvider : IServiceProvider, ISupportRequiredService, IDisposable
    {
        // Fields
        private AutofacContext _autofacContext = null;

        private AutofacServiceProvider _autofacServiceProvider = null;


        // Constructors
        public LazyAutofacServiceProvider(AutofacContext autofacContext)
        {
            #region Contracts

            if (autofacContext == null) throw new ArgumentException();

            #endregion

            // Default
            _autofacContext = autofacContext;
        }

        public void Dispose()
        {
            // Dispose
            _autofacServiceProvider?.Dispose();
        }


        // Properties
        public AutofacServiceProvider AutofacServiceProvider
        {
            get
            {
                // Create
                if (_autofacServiceProvider == null)
                {
                    _autofacServiceProvider = new AutofacServiceProvider(_autofacContext.Container);
                }

                // Return
                return _autofacServiceProvider;
            }
        }


        // Methods
        public object GetService(Type serviceType)
        {
            #region Contracts

            if (serviceType == null) throw new ArgumentException();

            #endregion

            // Return
            return this.AutofacServiceProvider.GetService(serviceType);
        }

        public object GetRequiredService(Type serviceType)
        {
            #region Contracts

            if (serviceType == null) throw new ArgumentException();

            #endregion

            // Return
            return this.AutofacServiceProvider.GetRequiredService(serviceType);
        }
    }
}
