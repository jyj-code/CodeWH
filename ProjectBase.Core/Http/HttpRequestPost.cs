using System;
using System.Net;
using System.Text;

namespace ProjectBase.Core.Http
{
    public class HttpRequestPost
    {
        #region 通过post的方式请求html信息
        /// <summary>
        /// 通过post的方式请求获取html信息
        /// </summary>
        /// <param name="url">要请求的地址</param>
        /// <param name="postData">post数据</param>
        /// <param name="encodeType">编码格式</param>
        /// <param name="err">返回错误信息</param>
        /// <returns></returns>
        internal static string Post_Http(string url, string postData, string encodeType, out string err)
        {
            string uriString = url;
            byte[] byteArray;
            byte[] responseArray;
            Encoding encoding = Encoding.GetEncoding(encodeType);
            try
            {
                WebClient myWebClient = CreateWebClient();
                WebHeaderCollection myWebHeaderCollection;
                myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                myWebHeaderCollection = myWebClient.Headers;
                byteArray = encoding.GetBytes(postData);
                responseArray = myWebClient.UploadData(uriString, "POST", byteArray);
                err = string.Empty;
                return encoding.GetString(responseArray).ToString();
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return string.Empty;
            }
        }
        #endregion

        #region 根据是否使用代理配置，实例化一个WebClient对象
        /// <summary>
        /// 根据是否使用代理配置，实例化一个WebClient对象
        /// </summary>
        /// <returns></returns>
        internal static WebClient CreateWebClient()
        {
            WebClient wec = new WebClient();
            if (StaticParameters.proxyEnable)
            {
                wec.Proxy = new WebProxy(StaticParameters.proxyServer, StaticParameters.proxyPort);
            }
            return wec;
        }
        #endregion
    }
}
