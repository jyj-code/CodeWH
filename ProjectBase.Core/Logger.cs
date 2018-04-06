using log4net;
using log4net.Config;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace KebueLog
{
    public sealed class Logger
    {
        /// <summary>
        /// 记录消息Queue
        /// </summary>
        private readonly ConcurrentQueue<LogMessage> _que;

        /// <summary>
        /// 信号
        /// </summary>
        private readonly ManualResetEvent _mre;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// 日志
        /// </summary>
        private static Logger _flashLog = new Logger();


        private Logger()
        {
            // 设置日志配置文件路径
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));

            _que = new ConcurrentQueue<LogMessage>();
            _mre = new ManualResetEvent(false);
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// 实现单例
        /// </summary>
        /// <returns></returns>
        public static Logger Instance()
        {
            return _flashLog;
        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// 在程序启动时注册，如asp.net 程序中在Global.asax中的Application_Start注册。  KebueLog.Logger.Instance().Register();
        /// </summary>
        public void Register()
        {
            Thread t = new Thread(new ThreadStart(WriteLog));
            t.IsBackground = false;
            t.Start();
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        private void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                _mre.WaitOne();

                LogMessage msg;
                // 判断是否有内容需要如磁盘 从列队中获取内容，并删除列队中的内容
                while (_que.Count > 0 && _que.TryDequeue(out msg))
                {
                    // 判断日志等级，然后写日志
                    switch (msg.Level)
                    {
                        case LogLevel.Debug:
                            _log.Debug(msg.Message, msg.Exception);
                            break;
                        case LogLevel.Info:
                            _log.Info(msg.Message, msg.Exception);
                            break;
                        case LogLevel.Error:
                            _log.Error(msg.Message, msg.Exception);
                            break;
                        case LogLevel.Warn:
                            _log.Warn(msg.Message, msg.Exception);
                            break;
                        case LogLevel.Fatal:
                            _log.Fatal(msg.Message, msg.Exception);
                            break;
                    }
                }
                // 重新设置信号
                _mre.Reset();
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        public void EnqueueMessage(string message, LogLevel level, Exception ex = null)
        {
            if ((level == LogLevel.Debug && _log.IsDebugEnabled)
             || (level == LogLevel.Error && _log.IsErrorEnabled)
             || (level == LogLevel.Fatal && _log.IsFatalEnabled)
             || (level == LogLevel.Info && _log.IsInfoEnabled)
             || (level == LogLevel.Warn && _log.IsWarnEnabled))
            {
                _que.Enqueue(new LogMessage
                {
                    Message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + "]\r\n" + message,
                    Level = level,
                    Exception = ex
                });

                // 通知线程往磁盘中写日志
                _mre.Set();
            }
        }
        /// <summary>
        ///   FlashLogger.Debug("Debug");
        //    FlashLogger.Debug("Debug", new Exception("testexception"));
        //    FlashLogger.Info("Info");
        //    FlashLogger.Fatal("Fatal");
        //    FlashLogger.Error("Error");
        //    FlashLogger.Warn("Warn", new Exception("testexception"));
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogLevel.Debug, ex);
        }

        public static void Error(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogLevel.Error, ex);
        }

        public static void Fatal(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogLevel.Fatal, ex);
        }

        public static void Info(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogLevel.Info, ex);
        }

        public static void Warn(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogLevel.Warn, ex);
        }

    }
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        Debug,
        Info,
        Error,
        Warn,
        Fatal
    }
    /// <summary>
    /// 日志内容
    /// </summary>
    public class LogMessage
    {
        public string Message { get; set; }
        public LogLevel Level { get; set; }
        public Exception Exception { get; set; }

    }
}