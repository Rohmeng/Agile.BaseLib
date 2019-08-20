using System;
using NLog;

namespace Agile.BaseLib.Helpers
{
    public class Logs
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static void WriteError(Exception exception, string msg = "")
        {
            logger.Error(exception, msg);
        }

        public static void WriteInfo(string msg = "")
        {
            logger.Info(msg);
        }
    }


}
