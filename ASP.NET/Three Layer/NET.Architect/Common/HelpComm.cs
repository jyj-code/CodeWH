using System;
using System.Diagnostics;

namespace NET.Architect.Common
{
    public class HelpComm
    {
        /// <summary>
        /// 获取操作系统的名字
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string GetOSNameByUserAgent(string userAgent, out string modelType)
        {
            #region 手机
            modelType = "Mobile";
            if (userAgent.Contains("Android"))
            {
                return "Android";
            }
            else if (userAgent.Contains("iPhone"))
            {
                return "iPhone";
            }
            else if (userAgent.Contains("SymbianOS"))
            {
                return "SymbianOS";
            }
            else if (userAgent.Contains("Windows Phone"))
            {
                return "Windows Phone";
            }
            else if (userAgent.Contains("iPad"))
            {
                return "iPad";
            }
            else if (userAgent.Contains("iPod"))
            {
                return "iPod";
            }
            #endregion
            #region 电脑   
            else if (userAgent.Contains("NT 10.0"))
            {
                modelType = "PC";
                return "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                modelType = "PC";
                return "Windows 8.1";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                modelType = "PC";
                return "Windows 8";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                modelType = "PC";
                return "Windows 7";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                modelType = "PC";
                return "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                modelType = "PC";
                return "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                modelType = "PC";
                if (userAgent.Contains("64"))
                    return "Windows XP";
                else
                    return "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                modelType = "PC";
                return "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                modelType = "PC";
                return "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                modelType = "PC";
                return "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                modelType = "PC";
                return "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                modelType = "PC";
                return "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                modelType = "PC";
                return "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                modelType = "PC";
                return "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                modelType = "PC";
                return "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                modelType = "PC";
                return "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                modelType = "PC";
                return "SunOS";
            }
            else
            {
                modelType = "PC";
                var osVersion = System.Web.HttpContext.Current.Request.Browser.Platform;
                return osVersion == null ? "未知" : osVersion;
            }
            #endregion
        }

        public static T JsonToObject<T>(string jsonString)
        {
            //T obj = System.Activator.CreateInstance<T>();
            try
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
                {
                    System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                    return (T)serializer.ReadObject(ms);
                }
            }
            catch
            {
                return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<T>(jsonString);
            }
        }

        /// <summary>
        /// 返回对象执行时间
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Stopwatch Stopwatch(Action input)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            input();
            watch.Stop();
            return watch;
        }
        public static Stopwatch Stopwatch<T1,T2>(Func<T1,T2> func,T1 t1, ref T2 t2)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                t2=func(t1);
            }
            finally
            {
                watch.Stop();
            }
            return watch;
        }
        //public static Stopwatch Stopwatch<T1, T2>(Func<T1, T2, int> func, T1 a, T2 b)
        //{
        //    Stopwatch watch = new Stopwatch();
        //    try
        //    {
        //        watch.Start();
        //        func(a, b);
        //    }
        //    finally
        //    {
        //        watch.Stop();
        //    }
        //    return watch;
        //}

        public static Stopwatch Stopwatch<T1>(Action<T1> func, T1 t1)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                func(t1);
            }
            finally
            {
                watch.Stop();
            }
            return watch;
        }
    }
}
