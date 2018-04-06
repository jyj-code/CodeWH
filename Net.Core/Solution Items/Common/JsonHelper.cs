using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class JsonHelper<T>
    {
        /// <summary>
        /// Json转对象
        /// </summary>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        public static List<T> JsonToList(string JsonStr)
        {
            return JsonConvert.DeserializeObject<List<T>>(JsonStr);
        }
    }
    public class JsonHelper
    {
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
