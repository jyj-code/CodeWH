using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// API请求对象
    /// </summary>
    public class APIResponse
    {
        public static string ApiUrl = "http://localhost:5000/api/";
        /// <summary>
        /// 数据请求类型
        /// </summary>
        public enum ResponseType
        {
            Get,
            Post,
            Put,
            Delete
        }

        public static string HttpLinkURL(string action,string Methor=null,string param=null)
        {
            StringBuilder str = new StringBuilder();
            str.Append(ApiUrl);
            str.Append(action);
            if(Methor!=null)
                str.Append("/"+ Methor);
            if (param != null)
                str.Append("?"+ param);
            return str.ToString();
        }
        /// <summary>
        /// API请求
        /// </summary>
        /// <param name="type">请求方法 Get获取，Post新增，Put 更新，Delete删除</param>
        /// <param name="action">请求Action</param>
        /// <param name="content">请求参数</param>
        /// <returns></returns>
        public static string ResponseAPI(ResponseType type,string url, Object content = null)
        {
            //Response.AppendHeader("Access-Control-Allow-Origin", "*");
            //Response.Write("data");


            var httpClient = new HttpClient();

            switch (type)
            {
                case ResponseType.Get:
                    return httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                case ResponseType.Post:
                    return httpClient.PostAsync(url, new StringContent(JsonHelper.ObjectToJson(content))).Result.Content.ReadAsStringAsync().Result;
                case ResponseType.Put:
                    return httpClient.PutAsync(url, new StringContent(JsonHelper.ObjectToJson(content))).Result.Content.ReadAsStringAsync().Result;
                case ResponseType.Delete:
                    return httpClient.DeleteAsync(url).Result.Content.ReadAsStringAsync().Result;
                default: return null;
            }
        }
    }
}
