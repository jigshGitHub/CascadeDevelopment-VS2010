using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
namespace Cascade.Helpers
{
    public static class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool IsInfoEnabled()
        {
            return log.IsDebugEnabled;
        }

        public static void Debug(string message)
        {
            try
            {
                log.Debug(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Error(string message, Exception exParam)
        {
            try
            {
                log.Error(message, exParam);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Info(string message)
        {
            try
            {
                log.Info(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public static class ErrorLogHelper
    {
        private static readonly ILog log;

        static ErrorLogHelper()
        {
            if (log == null)
            {
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(System.Configuration.ConfigurationManager.AppSettings["errorConfigFilePath"]));
                log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }
        public static void Error(string message, Exception exParam)
        {
            try
            {
                log.Error(message, exParam);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
