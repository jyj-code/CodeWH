using NET.Architect;
using NET.Architect.Busioness;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace UI
{
    public class Requests
    {
        /// <summary>
        /// Get HTTP请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="param">请求参数</param>
        /// <param name="apikey"></param>
        /// <param name="apikeyValue"></param>
        /// <returns></returns>
        public string getRequest(string url, string param, string apikey, string apikeyValue)
        {
            string StrDate = string.Empty;
            StringBuilder strValue = new StringBuilder();
            StringBuilder strURL = new StringBuilder();
            strURL.Append(url);
            strURL.Append("?");
            strURL.Append(param);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL.ToString());
            request.Method = "GET";
            request.Headers[apikey] = apikeyValue;
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponseAsync().Result;
            System.IO.Stream stream = response.GetResponseStream();
            System.IO.StreamReader Reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue.Append(StrDate + "\r\n");
            }
            return strValue.ToString();
        }
        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        public static Tutusoft Request_tutusoft(string param)
        {
            string url = "http://apis.baidu.com/tutusoft/shajj/shajj";
            param = string.Format("content={0}", param);
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            // 添加header
            request.Headers.Add("apikey", "ddc3e074ebbe2a7a01538056daa1c8ee");
            request.ContentType = "application/x-www-form-urlencoded";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return JsonConvert.DeserializeObject<Tutusoft>(strValue);
        }
      
    }

    #region 百度API解析IP 经度  //{"data":{"city":"苏州市","country":"China","ip":"112.87.137.223","lat":"120.61990712","lng":"31.31798737","operator":"","province":"江苏省"},"error":0,"msg":"succeed"}
    public class Address : RegionalCascadeAddres
    {

    }
    public class Root
    {
        /// <summary>
        /// Data
        /// </summary>
        public IpLocation data { get; set; }
        /// <summary>
        /// Error
        /// </summary>
        public int error { get; set; }
        /// <summary>
        /// succeed
        /// </summary>
        public string msg { get; set; }
    }
    #endregion
    /// <summary>
    /// 兔兔不良信息识别
    /// </summary>
    public class Tutusoft
    {
        /// <summary>
        /// 1
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 9
        /// </summary>
        public string categoryId { get; set; }
        /// <summary>
        /// 违法信息
        /// </summary>
        public string categoryName { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string nature { get; set; }
        /// <summary>
        /// 出售+安乐死药:qq
        /// </summary>
        public string words { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

}