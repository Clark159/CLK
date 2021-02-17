using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static log4net.Appender.RollingFileAppender;

namespace CLK.Logging.Log4net
{
    public class Log4netLoggerFactory : LoggerFactory
    {
        // Constructors
        public Log4netLoggerFactory(string configFilename = null)
        {
            // Setting
            log4net.MDC.Set("tab", "\t");

            // ConfigFileList
            List<FileInfo> configFileList = new List<FileInfo>();
            if (string.IsNullOrEmpty(configFilename) == false)
            {
                // GetAll
                configFileList = FileHelper.GetAllFile(configFilename);
                if (configFileList == null) throw new InvalidOperationException("configFileList=null");
            }

            // Configure
            if (configFileList.Count > 0)
            {
                // Repository
                var repository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
                if (repository == null) throw new InvalidOperationException("repository=null");

                // XmlConfigurator
                foreach (var configFile in configFileList)
                {
                    XmlConfigurator.Configure(repository, configFile);
                }
            }
            else
            {
                // Repository
                var repository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
                if (repository == null) throw new InvalidOperationException("repository=null");

                // PatternLayout
                var patternLayout = new log4net.Layout.PatternLayout()
                {
                    ConversionPattern = @"%date{yyyy-MM-dd HH:mm:ss fff} %-5level [%thread] %logger.%property{method}() - %message%newline"
                };
                patternLayout.ActivateOptions();

                // ConsoleAppender
                var consoleAppender = new log4net.Appender.ConsoleAppender()
                {
                    Layout = patternLayout
                };
                consoleAppender.ActivateOptions();

                // FileAppender
                var fileAppender = new log4net.Appender.RollingFileAppender()
                {
                    Layout = patternLayout,
                    RollingStyle = RollingMode.Date,
                    File = string.Format("log/{0}", System.Reflection.Assembly.GetEntryAssembly().GetName().Name),
                    DatePattern = " yyyy-MM-dd'.log'",
                    StaticLogFileName = false,
                    AppendToFile = true
                };
                fileAppender.ActivateOptions();

                // BasicConfigurator
                BasicConfigurator.Configure(repository, 
                    consoleAppender, 
                    fileAppender
                );
            }
        }

        public void Dispose()
        {

        }


        // Methods
        public LoggerProvider Create<TCategory>()
        {
            // Logger
            var logger = LogManager.GetLogger(System.Reflection.Assembly.GetEntryAssembly(), typeof(TCategory));
            if (logger == null) throw new InvalidOperationException("logger=null");

            // Return
            return new Log4netLoggerProvider(logger);
        }
    }
}
