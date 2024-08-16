using log4net;
using log4net.Config;
using System.Reflection;

namespace Adroid.Serivces.Common
{
    public class LogHelper
    {
        private const string UAT_FOLDER_NAME = "UAT";
        private const string STAGING_FOLDER_NAME = "STAGING";
        private const string PROD_FOLDER_NAME = "PROD";
        public static void SetUpLoggingEnvironment(string appName)
        {
            GlobalContext.Properties["ENVIRONMENT"] = UAT_FOLDER_NAME;
            GlobalContext.Properties["APPNAME"] = appName;
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            var log4netFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"log4net.config");
            XmlConfigurator.Configure(logRepository,new FileInfo(log4netFilePath));
        }
    }
}
