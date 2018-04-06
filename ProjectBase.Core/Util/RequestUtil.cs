//
// 请注意：bbsmax 不是一个免费产品，源代码仅限用于学习，禁止用于商业站点或者其他商业用途
// 如果您要将bbsmax用于商业用途，需要从官方购买商业授权，得到授权后可以基于源代码二次开发
using System;
using System.Web;
using System.IO;
using System.Web.Caching;

namespace ProjectBase.Core.Util
{
    public class RequestUtil
    {
        /// <summary>
        /// 获得浏览器名称（包括版本号）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBrowserName(HttpRequest request)
        {
            HttpBrowserCapabilities browser = request.Browser;

            if (browser == null)
                return "Unknown";

            string text;
            if (browser.Browser == "IE")
            {
                if (browser.Beta)
                    text = string.Concat("Internet Explorer ", browser.Version, "(beta)");
                else
                    text = "Internet Explorer " + browser.Version;
            }
            else
            {
                string userAgent = request.UserAgent;

                if (userAgent != null && userAgent.IndexOf("Chrome") != -1)
                    text = "Chrome";
                else if (userAgent != null && userAgent.IndexOf("Safari") != -1)
                    text = "Safari";
                else if (browser.Beta)
                    text = string.Concat(browser.Browser, " ", browser.Version, "(beta)");
                else
                    text = string.Concat(browser.Browser, " ", browser.Version);
            }

            return text;

        }

        #region GZIP压缩相关
        static float GetQuality(string acceptEncodingValue)
        {
            int qParam = acceptEncodingValue.IndexOf("q=", StringComparison.OrdinalIgnoreCase);

            if (qParam >= 0)
            {
                float val = 0.0f;
                try
                {
                    val = float.Parse(acceptEncodingValue.Substring(qParam + 2, acceptEncodingValue.Length - (qParam + 2)));
                }
                catch (FormatException)
                {

                }
                return val;
            }
            else
                return 1;
        }


        public class FastAspxCacheData
        {
            public byte[] Data;
            public DateTime LastModified;
        }
        const string INSTALLED_KEY = "httpcompress.attemptedinstall";
        static readonly object INSTALLED_TAG = new object();
        public static void SetInstalledKey(HttpContext content)
        {
            if (content.Items.Contains(INSTALLED_KEY))
                content.Items[INSTALLED_KEY] = INSTALLED_TAG;
            else
                content.Items.Add(INSTALLED_KEY, INSTALLED_TAG);
        }
        public static bool ContainsInstalledKey(HttpContext content)
        {
            if (content.Items.Contains(INSTALLED_KEY))
                return true;
            else
                return false;
        }

        #endregion
    }

    public enum CompressingType
    {
        GZip, Deflate, None
    }
}