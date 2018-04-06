using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NET.Architect.Common
{
    public static class ExtendMethod
    {
       
        public static System.Reflection.PropertyInfo[] GetPropertyInfo<T>(this T t)
        {
            return t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        }
        #region 截取
        /// <summary>
        /// 截取最后一位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LnterceptLastOne(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            str = str.Trim();
            if (str.Length < 1) return str;
            return str.Substring(0, str.Length - 1);
        }
        public static string LnterceptLastTwo(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            str = str.Trim();
            if (str.Length < 2) return str;
            return str.Substring(0, str.Length - 2);
        }
        public static string LnterceptLastThree(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            str = str.Trim();
            if (str.Length < 3) return str;
            return str.Substring(0, str.Length - 3);
        }
        #endregion

        #region 将对象转换成Json
        /// <summary>
        /// 将对象转换成Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToConvertJson(this object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        } 
        #endregion

        #region 半角转全角
        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(this string input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }
        #endregion
        /// <summary>
        /// 如果不等于空返回true
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Is(this string input)
        {
            if (input != null)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    if (input.Length > 0)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 大于0返回true
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Is(this int input)
        {
            if (input >0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 对象不等null返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Is<T>(this T input)
        {
            if (input!=null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 返回对象执行时间
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Stopwatch Stopwatch(this Func<object> input)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            input();
            watch.Stop();
            return watch;
        }
        /// <summary>
        /// 返回对象执行时间
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Stopwatch Stopwatch(this Action input)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            input();
            watch.Stop();
            return watch;
        }
        /// <summary>
        /// 返回对象执行时间
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="func"></param>
        /// <param name="t1"></param>
        /// <returns></returns>
        public static Stopwatch Stopwatch<T1, T2>(this Func<T1, T2> func, T1 t1)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            func(t1);
            watch.Stop();
            return watch;
        }
   
    }
}
