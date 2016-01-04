using System;
using log4net;

namespace OrbitOne.BuildScreen.Services
{
    public class LogService
    {
        private static ILog Logger = LogManager.GetLogger(typeof(LogService));

        public static void WriteInfo(string message)
        {
            Logger.Info(message);
        }

        public static void WriteError(Exception ex)
        {
            Logger.ErrorFormat("Exception {0} \n {1}", ex.Message, ex.StackTrace);
            LogAggregateException(ex as AggregateException);
        }

        private static void LogAggregateException(AggregateException ae)
        {
            if (ae != null)
            {
                WriteInfo("AggregateException");
                foreach (var ex in ae.InnerExceptions)
                {
                    WriteInfo(ex.Message);
                    Logger.ErrorFormat("Exception {0} \n {1}", ex.Message, ex.StackTrace);
                    LogAggregateException(ex as AggregateException);
                }
            }
        }

    }
}
