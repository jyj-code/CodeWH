using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ProjectBase.Core.Http
{
    public class HttpRequestGet
    {
        #region 通过get的方式请求得到页面信息，参数1：url ，参数2：编码格式
        public static string Get_Http(string tUrl, string encodeType)
        {
            string strResult;
            try
            {
                HttpWebRequest hwr = CreateHttpWebRequest(tUrl);
                hwr.Timeout = 19600;
                CookieContainer cc = new CookieContainer();
                hwr.CookieContainer = cc;
                HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();
                Stream myStream = hwrs.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding(encodeType);
                StreamReader sr = new StreamReader(myStream, encoding);
                StringBuilder sb = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    sb.Append(sr.ReadLine() + "\r\n");
                }
                strResult = sb.ToString();
                hwrs.Close();
            }
            catch (Exception ex)
            {
                strResult = string.Format("Exception:{0},StackTrace:{1}", ex.Message, ex.StackTrace);
            }
            return strResult;
        }
        public static string Get_Https(string tUrl, string encodeType)
        {
            string strResult;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);//验证服务器证书回调自动验证
                HttpWebRequest hwr = CreateHttpWebRequest(tUrl);
                hwr.UserAgent = StaticParameters.UserAgent;
                hwr.Timeout = 19600;
                CookieContainer cc = new CookieContainer();
                hwr.CookieContainer = cc;
                HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();
                Stream myStream = hwrs.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding(encodeType);
                StreamReader sr = new StreamReader(myStream, encoding);
                strResult = sr.ReadToEnd();
                sr.Close();
                hwrs.Close();
            }
            catch (Exception ex)
            {
                strResult = string.Format("Exception:{0},StackTrace:{1}", ex.Message, ex.StackTrace);
            }
            return strResult;
        }
        public static string Get_HttpAll(string tUrl, string encodeType)
        {
            string strResult;
            try
            {
                HttpWebRequest hwr = CreateHttpWebRequest(tUrl);
                hwr.Timeout = 19990;
                CookieContainer cc = new CookieContainer();
                hwr.CookieContainer = cc;
                HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();
                Stream myStream = hwrs.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding(encodeType);
                StreamReader sr = new StreamReader(myStream, encoding);
                strResult = sr.ReadToEnd();
                hwrs.Close();
            }
            catch (Exception ex)
            {
                strResult = string.Format("Exception:{0},StackTrace:{1}", ex.Message, ex.StackTrace);
            }
            return strResult;
        }
   

        #endregion





        #region 根据是否使用代理配置，创建一个HttpWebRequest对象
        /// <summary>
        /// 根据是否使用代理配置，创建一个HttpWebRequest对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal static HttpWebRequest CreateHttpWebRequest(string url)
        {
            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(url);
            if (StaticParameters.proxyEnable)
            {
                hwr.Proxy = new WebProxy(StaticParameters.proxyServer, StaticParameters.proxyPort);
            }
            return hwr;
        }
        #endregion

        #region 针对的https调用中“基础连接已经关闭: 未能为 SSL/TLS 安全通道建立信任关系”的异常”而改进
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        { // 总是接受 
            return true;
        }
        #endregion
    }
}
