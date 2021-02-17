using System;
using System.IO;
using CLK.Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CLK.AspNetCore
{
    public class AspnetContext : IDisposable
    {
        // Fields
        private WebHostBuilder _webHostBuilder = null;

        private IWebHost _webHost = null;


        // Constructors
        public AspnetContext(AutofacContext autofacContext, string listenUrl, string controllerFilename)
        {
            #region Contracts

            if (autofacContext == null) throw new ArgumentException();
            if (string.IsNullOrEmpty(listenUrl) == true) throw new ArgumentException();
            if (string.IsNullOrEmpty(controllerFilename) == true) throw new ArgumentException();

            #endregion

            // WebHostBuilder
            _webHostBuilder = new WebHostBuilder(autofacContext, listenUrl, controllerFilename);
        }

        public void Start()
        {
            // Require
            if (_webHost != null) return;

            // WebHost
            _webHost = _webHostBuilder.Create();
            if (_webHost == null) throw new InvalidOperationException("_webHost=null");

            // Start
            _webHost.Start();
        }

        public void Dispose()
        {
            // Require
            if (_webHost == null) return;

            // Dispose
            _webHost.Dispose();
        }


        // Class
        private class WebHostBuilder : IStartup
        {
            // Fields
            private readonly AutofacContext _autofacContext = null;

            private readonly string _listenUrl = null;

            private readonly string _controllerFilename = null;


            // Constructors
            public WebHostBuilder(AutofacContext autofacContext, string listenUrl, string controllerFilename)
            {
                #region Contracts

                if (autofacContext == null) throw new ArgumentException();
                if (string.IsNullOrEmpty(listenUrl) == true) throw new ArgumentException();
                if (string.IsNullOrEmpty(controllerFilename) == true) throw new ArgumentException();

                #endregion

                // Default
                _autofacContext = autofacContext;
                _listenUrl = listenUrl;
                _controllerFilename = controllerFilename;
            }


            // Methods
            public IWebHost Create()
            {
                // Create
                IWebHost webHost = null;
                {
                    // Builder
                    webHost = new Microsoft.AspNetCore.Hosting.WebHostBuilder()

                    // Services
                    .ConfigureServices((services) =>
                    {
                        // Add
                        services.AddSingleton<IStartup>(this);
                    })

                    // Content
                    .UseContentRoot(Directory.GetCurrentDirectory())

                    // Server
                    .UseKestrel()

                    // Listen
                    .UseUrls(_listenUrl)

                    // Build       
                    .Build();
                }
                if (webHost == null) throw new InvalidOperationException("webHost=null");

                // Return
                return webHost;
            }

            public void Configure(IApplicationBuilder app)
            {
                #region Contracts

                if (app == null) throw new ArgumentException();

                #endregion

                // Cors
                app.UseCors("Default");

                // Exception
                app.UseExceptionMiddleware();

                // FileServer
                app.UseFileServer();

                // Mvc   
                app.UseMvc();
            }

            public IServiceProvider ConfigureServices(IServiceCollection services)
            {
                #region Contracts

                if (services == null) throw new ArgumentException();

                #endregion

                // Cors
                services.AddCors(options => options.AddPolicy("Default", corsPolicyBuilder => corsPolicyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                ));

                // Mvc
                services.AddMvcCore(options =>
                {
                    // Conventions
                    options.Conventions.Clear();
                    options.Conventions.AddNamespaceRoute();

                    // Filters
                    options.Filters.AddActionLogger();
                })
                .AddJsonFormatters()
                .AddAssemblyController(_controllerFilename);

                // ServiceProvider
                IServiceProvider serviceProvider = null;
                {
                    // Register
                    _autofacContext.RegisterServices(services);

                    // Create
                    serviceProvider = new LazyAutofacServiceProvider(_autofacContext);
                }

                // Return
                return serviceProvider;
            }
        }        
    }
}
