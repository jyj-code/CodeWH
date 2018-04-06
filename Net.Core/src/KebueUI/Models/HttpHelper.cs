using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KebueUI.Models
{
    public class HttpHelper
    {
        #region HttpGet
        /// <summary>
        /// 使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, Encoding encoding = null)
        {
            HttpClient httpClient = new HttpClient();
            var data = await httpClient.GetByteArrayAsync(url);
            var ret = encoding.GetString(data);
            return ret;
        }
        /// <summary>
        /// Http Get 同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding encoding = null)
        {
            HttpClient httpClient = new HttpClient();
            var t = httpClient.GetByteArrayAsync(url);
            t.Wait();
            return encoding.GetString(t.Result);
        }
        #endregion
        #region HttpPost方法
        /// <summary>
        /// POST 异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new StreamContent(ms);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");
            var r = await client.PostAsync(url, hc);
            byte[] tmp = await r.Content.ReadAsByteArrayAsync();
            return encoding.GetString(tmp);
        }

        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new StreamContent(ms);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");
            var t = client.PostAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            return encoding.GetString(t2.Result);
        }
        public static string HttpWebRequests(string requestURL, Dictionary<object, object> requestpare)
        {
            string responseText = string.Empty;
            StringBuilder str = new StringBuilder();
            str.Append(requestURL);
            foreach (var item in requestpare)
            {
                str.AppendFormat("&{0}={1}", item.Key, item.Value);
            }

            var response = System.Net.WebRequest.Create(str.ToString()).GetResponseAsync();
            using (System.IO.StreamReader myreader = new System.IO.StreamReader(response.Result.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                responseText = myreader.ReadToEnd();
            }
            return responseText;
        }
        public static string RequestTuringServices(string info, ChatRequest param)
        {
            TuringParea pareas = TuringParea.GetTuringParea();
            pareas.info = info;
            pareas.loc = param.mapEntity.content.formatted_address;
            pareas.lon = param.mapEntity.content.location.Lng;
            pareas.lat = param.mapEntity.content.location.Lat;
            pareas.userid = param.Ip.GetHashCode();
            return HttpWebRequests(constStr.Turing_url, Newtonsoft.Json.JsonConvert.SerializeObject(pareas));
        }
        public static string HttpWebRequests(string url, string param)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/json; charset=UTF-8";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStreamAsync().Result))
            {
                dataStream.Write(param);
            }
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponseAsync().Result;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            return reader.ReadToEnd();
        }

        #endregion
    }
    public class TuringParea
    {
        public static TuringParea GetTuringParea()
        {
            return new TuringParea()
            {
                key = constStr.Turing_key
            };
        }
        public string key { get; set; }
        public string info { get; set; }
        public string loc { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
        public int userid { get; set; }
    }
}
