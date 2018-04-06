using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtendMethod
    {
        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string UrlDecode2(this string Url)
        {
            return System.Net.WebUtility.UrlDecode(Url);
        }
        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        public static List<T> ToObjectList<T>(this string str)
        {
            return JsonConvert.DeserializeObject<List<T>>(str);
        }
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToString<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
        public static string ObjToString<T>(this List<T> t)
        {
            return JsonConvert.SerializeObject(t);
        }

    }
}
