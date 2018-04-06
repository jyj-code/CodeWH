using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectBase.Core.Util
{
    public class UrlUtil
    {
        private readonly static string[] s_Domains_CN = new string[] { "com", "net", "org", "gov", "edu", "co", "中国", "公司", "网络", "ac", "bj", "sh", "tj", "cq", "he", "sx", "nm", "ln", "jl", "hl", "js", "zj", "ah", "fj", "jx", "sd", "ha", "hb", "hn", "gd", "gx", "hi", "sc", "gz", "yn", "xz", "sn", "gs", "qh", "nx", "xj", "tw", "hk", "mo" };
        private readonly static string[] s_Domains_RU = new string[] { "com", "net", "org", "gov", "edu", "co", "pp" };

        private static Regex s_LocalRegex = null;

        private static Dictionary<string, KeyValuePair<string, string>> s_CachedMainDomains = new Dictionary<string, KeyValuePair<string, string>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 移除路径中的.和..，并转换成其对应的真实路径。
        /// 例如 aa/bb/../cc 将被处理为 aa/cc
        /// </summary>
        /// <returns></returns>
        public static string ConvertDots(string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            if (url.Contains("./") == false || url.Contains("/") == false)
                return url;

            string[] parts = url.Split('/');

            List<string> builder = new List<string>();

            foreach (string part in parts)
            {
                if (part == ".")
                    continue;

                else if (part == "..")
                {
                    if (builder.Count <= 0 || (builder.Count == 1 && builder[0] == ""))
                        throw new Exception("当前提供的路径中的..已经超出了路径的范围，无法处理");


                    builder.RemoveAt(builder.Count - 1);
                }

                else
                    builder.Add(part);
            }

            StringBuilder urlBuilder = new StringBuilder();

            bool isFirst = true;
            foreach (string newItem in builder)
            {
                if (isFirst == true)
                    isFirst = false;
                else
                    urlBuilder.Append("/");

                urlBuilder.Append(newItem);
            }

            return urlBuilder.ToString();
        }

        private static readonly char[] urlTrimChars = new char[] { '/', '\\' };

        /// <summary>
        /// 拼接两个Url，不管url1的结尾是否包含/或\，也不管url2的开头是否包含/或\，都能正确地拼接
        /// </summary>
        /// <param name="url1"></param>
        /// <param name="url2"></param>
        /// <returns></returns>
        public static string JoinUrl(string url1, string url2)
        {
            string url = string.Concat(url1.TrimEnd(urlTrimChars), "/", url2.TrimStart(urlTrimChars));
            return url.Replace('\\', '/');
        }

        public static string JoinUrl(string url1, string url2, string url3)
        {
            string url = string.Concat(url1.TrimEnd(urlTrimChars), "/", url2.Trim(urlTrimChars), "/", url3.TrimStart(urlTrimChars));
            //while (url.Contains("//"))
            //{
            //    url = url.Replace("//", "/");
            //}
            return url.Replace('\\', '/');
        }

        /// <summary>
        /// 拼接多个Url，不管前一个url的结尾是否包含/或\，也不管后一个url的开头是否包含/或\，都能正确地拼接
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public static string JoinUrl(params string[] urls)
        {

            if (urls == null || urls.Length == 0)
                return string.Empty;

            else if (urls.Length == 1)
                return urls[0];

            StringBuilder builder = new StringBuilder();

            int i = 0;
            foreach (string url in urls)
            {
                if (string.IsNullOrEmpty(url) == false)
                {
                    if (i == 0)
                    {
                        builder.Append(url.TrimEnd(urlTrimChars));
                    }
                    else if (i == urls.Length - 1)
                    {
                        builder.Append("/");
                        builder.Append(url.TrimStart(urlTrimChars));
                    }
                    else
                    {
                        builder.Append("/");
                        builder.Append(url.Trim(urlTrimChars));
                    }
                }
                i++;
            }
            return builder.Replace('\\', '/').ToString();
            //return builder.ToString();
        }
        public static string FormatLink(string link)
        {
            if (link.IndexOf("://") > 0)
                return link;
            else
                return "http://" + link;
        }

        public static string SafeUrl(string url)
        {
            StringBuilder builder = new StringBuilder(url);
            builder = builder.Replace("<", "%3C");
            builder = builder.Replace(">", "%3E");
            builder = builder.Replace("\"", "%22");
            builder = builder.Replace("'", "%27");
            return builder.ToString();
        }
        public static void Redirect(string url)
        {
            System.Web.HttpContext.Current.Response.Status = "301 Moved Permanently";
            System.Web.HttpContext.Current.Response.AddHeader("Location", url); 

        }

    }
}