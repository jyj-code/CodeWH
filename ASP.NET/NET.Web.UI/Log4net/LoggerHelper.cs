using System;
using System.Threading.Tasks;

namespace Log4net
{
    public class LoggerHelper
    {
        static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");
        static readonly log4net.ILog logmonitor = log4net.LogManager.GetLogger("logmonitor");
        public static void Error(string ErrorMsg, Exception ex = null)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                if (ex != null)
                    logerror.Error(ErrorMsg, ex);
                else
                    logerror.Error(ErrorMsg);
            });
        }
        public static void Info(string Msg)
        {
            Task task = Task.Factory.StartNew(() => { loginfo.Info(Msg); });
        }
        public static void Monitor(string Msg)
        {
            Task task = Task.Factory.StartNew(() => { logmonitor.Info(Msg); });
        }
    }
}