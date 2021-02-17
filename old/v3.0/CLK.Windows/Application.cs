using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using CLK.AspNetCore;
using CLK.Autofac;
using CLK.Logging;
using CLK.Logging.Log4net;

namespace CLK.Windows
{
    public class Application
    {
        // Methods
        public static void Run(Window window = null)
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
                var windowContext = new WindowContext(autofacContext, window);
                Action startAction = () =>
                {
                    aspnetContext?.Start();
                    windowContext?.Start();
                };
                Action endAction = () =>
                {
                    windowContext?.Dispose();
                    aspnetContext?.Dispose();
                    autofacContext?.Dispose();
                    loggerContext?.Dispose();
                };

                // Run  
                logger.Info("========================================");
                logger.Info(string.Format("Application started: appName={0}, appVersion={1}", appName, appVersion));
                startAction.Invoke();
                System.Windows.Application.Current.Exit += (s, e) =>
                {
                    endAction.Invoke();
                    logger.Info("Application ended");
                    logger.Info("========================================");
                };

                // Setting
                System.Windows.Application.Current.MainWindow.Title = string.Format("{0} ({1})", appName, appVersion);
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
                System.Windows.MessageBox.Show(string.Format("Application error: exception={0}", exception?.Message), "Application error");
            }
        }
    }
}
