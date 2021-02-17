using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CLK.Autofac;
using CLK.Logging;
using CLK.Logging.Log4net;

namespace CLK.AspNetCore
{
    public class Application
    {
        // Methods
        public static void Run()
        {
            // Loger
            var loggerContext = LoggerContext.Initialize(new Log4netLoggerFactory());
            var logger = new Logger<Application>();

            // Execute  
            try
            {
                // AppSettings
                var appSettings = SettingsHelper.GetAllAppSettings();
                if (appSettings == null) throw new InvalidOperationException("appSettings=null");

                // Variables
                var appName = appSettings["appName"];
                var appVersion = appSettings["appVersion"];
                var listenUrl = appSettings["appListen"];
                var hostingFilename = @"*.Hosting.json";
                var servicesFilename = @"*.Services.dll";
                
                // Context
                var autofacContext = new AutofacContext(hostingFilename);
                var aspnetContext = string.IsNullOrEmpty(listenUrl) == false ? new AspnetContext(autofacContext, listenUrl, servicesFilename) : null;
                Action startAction = () =>
                {
                    aspnetContext?.Start();
                };
                Action endAction = () =>
                {
                    aspnetContext?.Dispose();
                    autofacContext?.Dispose();
                    loggerContext?.Dispose();
                };

                // Setting
                Console.Title = string.Format("{0} ({1})", appName, appVersion);

                // Run  
                logger.Info("========================================");
                logger.Info(string.Format("Application started: appName={0}, appVersion={1}, appListen={2}", appName, appVersion, listenUrl));
                startAction.Invoke();
                {
                    var executeEvent = new ManualResetEvent(false);
                    Console.WriteLine("Press Ctrl + C to shut down.");
                    Console.CancelKeyPress += (s, e) => { executeEvent.Set(); e.Cancel = true; };
                    executeEvent.WaitOne();
                }
                endAction.Invoke();
                logger.Info("Application ended");
                logger.Info("========================================");
            }
            catch (Exception exception)
            {
                // Error
                while (exception?.InnerException != null)
                {
                    exception = exception.InnerException;
                }
                logger.Info(string.Format("Application error: exception={0}", exception?.Message), exception);
                logger.Info("========================================");

                // Notify
                var notifyEvent = new ManualResetEvent(false);
                Console.WriteLine("Press Ctrl + C to shut down.");
                Console.CancelKeyPress += (s, e) => { notifyEvent.Set(); e.Cancel = true; };
                notifyEvent.WaitOne();
            }
        }
    }
}
