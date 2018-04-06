using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIRequestServices
{
    public class BaidOrdinaryRequest
    {
        /// <summary>
        /// 根据IP 用百度API请求地址
        /// </summary>
        /// <param name="param">ip</param>
        /// <returns></returns>
        public static string BaidApiGetRequestIpAddress(string param)
        {
            string url = "http://apis.baidu.com/chazhao/ipsearch/ipsearch";
            string strURL = url + "?ip=" + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", "ddc3e074ebbe2a7a01538056daa1c8ee");
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
            return strValue;
        }
    }
}
