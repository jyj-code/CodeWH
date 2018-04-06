using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NET.Architect.Common
{
    public class StatTool
    {
        /// <summary>
        /// 站内统计
        /// </summary>
        public static int countTool { get; set; }
        public static string GetCurrentDirectory { get { return System.IO.Directory.GetCurrentDirectory(); } }
        /// <summary>
        /// 关键字过滤
        /// </summary>
        public static string FilterKey { get { return System.Configuration.ConfigurationManager.ConnectionStrings["FilterKey"].ConnectionString; } }
        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToConvertHalfAngle(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
        /// <summary>
        /// 转化为全角字符串
        /// </summary>
        /// <param name="input">要转化的字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToConvertFullWidth(string input)//double byte charactor 
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 危险词过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertFileStr(string str)
        {
            str = str.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                string filte1 = "<script";
                string filte2 = "</script>";
                filte1 = filte1.ToLower().Trim();
                filte2 = filte2.ToLower().Trim();
                if (str.ToLower().Contains(filte1) && str.ToLower().Contains(filte2))
                {
                    str = str.ToLower();
                    str = str.Replace(filte1, ToConvertFullWidth(filte1));
                    str = str.Replace(filte2, ToConvertFullWidth(filte2));
                }
            }
            return str;
        }
        public static bool KeyFilter(string input)
        {
            string[] str = StatTool.FilterKey.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                if (!string.IsNullOrEmpty(input.Trim()) && !string.IsNullOrEmpty(str[i].Trim()) && input.Contains(str[i]))
                {
                    return false;
                }
                else
                    continue;
            }
            return true;
        }
        #region##添加cookeis
        /// <summary>
        /// 添加cookeis
        /// </summary>
        /// <param name="key">Cookie键</param>
        /// <param name="value">Cookie值</param>
        /// <param name="ExpiresMinutes">Cookie过期分钟</param>
        public void AddCookies(string key, string value, int ExpiresMinutes)
        {
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
            HttpCookie cookie = new HttpCookie(HttpUtility.UrlEncode(key.ToString(), enc), HttpUtility.UrlEncode(value.ToString(), enc));
            cookie.Expires = DateTime.Now.AddMinutes(ExpiresMinutes);
        }
        /// <summary>
        /// 读取Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public String GetCookie(string key)
        {
            HttpCookie cookie = new HttpCookie(key);
            System.Text.Encoding encr = System.Text.Encoding.GetEncoding("gb2312");
            return HttpUtility.UrlDecode(cookie[key], encr);
        }
        #endregion
        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FileReader(string url)
        {
            StringBuilder str = new StringBuilder();
            if (File.Exists(url))
            {
                using (StreamReader sr = new StreamReader(url))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        str.Append(line.Trim());
                    }
                }
            }
            return str.ToString();
        }
        /// <summary>
        ///写文件
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="Path"></param>
        public static void FileWrite(string Context, string Path)
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            FileStream fs = new FileStream(Path, FileMode.Create);
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(Context);
            fs.Write(data, 0, data.Length);
            fs.Flush();
        }
        /// <summary>
        /// JS CSS文件压缩
        /// </summary>
        /// <param name="jsSourceCode"></param>
        /// <returns></returns>
        public static string CompactJavascript(string jsSourceCode)
        {
            var url = "http://tool.css-js.com/!java/?type=js&munge=true&preserveAllSemiColons=false&disableOptimizations=false";
            var web = (HttpWebRequest)WebRequest.Create(url);
            web.Method = "POST";
            web.Host = "tool.css-js.com";
            web.Accept = "text/plain";
            web.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36";
            web.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            web.Headers.Add("Accept-Language", "zh-CN,zh");
            var content = string.Format("code={0}", HttpUtility.UrlEncode(jsSourceCode));
            var data = Encoding.UTF8.GetBytes(content);
            using (var rq = web.GetRequestStream())
                rq.Write(data, 0, data.Length);
            using (var rs = web.GetResponse())
            using (var rd = rs.GetResponseStream())
            using (var rm = new StreamReader(rd))
            {
                var s = rm.ReadToEnd();
                return s;
            }
        }
        public static void Convert(string path)
        {
            var data = FileReader(path);
            data = ConvertKG(data);
            // CompactJavascript(data);
            string newName = "a1451d2ab4b9451c9594383af1f1d2c2";// Guid.NewGuid().ToString("N")
            var newPath = Path.GetDirectoryName(path) + @"\" + newName + Path.GetExtension(path);
            FileWrite(data, newPath);
        }
        /// <summary>
        /// 用递归替换空格
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertKG(string data)
        {
            string filter = "\r\n";
            if (data.Contains(filter))
                data = data.Replace(filter, " ");
            filter = "   ";//替换Table
            if (data.Contains(filter))
                data = data.Replace(filter, " ");
            filter = "  ";//替换2个空格
            if (data.Contains(filter))
                data = data.Replace(filter, " ");
            if (!data.Contains("  "))
                return data;
            return ConvertKG(data);
        }
        
        public static Dictionary<string, Properties> GetObjKeyValue<T>(T t)
        {
            Properties properties;
            Dictionary<string, Properties> dist = new Dictionary<string, Properties>();
            PropertyInfo[] propertyInfo = t.GetType().GetProperties(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.Instance);
            foreach (var item in propertyInfo)
            {
                properties = new Properties();
                properties.Value = item.GetValue(t, null) == null ? null : item.GetValue(t, null).ToString();
                properties.Attribute = item.GetCustomAttribute<DisplayNameAttribute>();
                properties.IsKey = item.GetCustomAttribute<KeyAttribute>() == null ? false : true;
                dist.Add(item.Name, properties);
            }
            return dist;
        }

        /// <summary>
        /// 获取公网IP
        /// </summary>
        public static string GetNetIp
        {
            get
            {
                string ip = string.Empty;
                try
                {
                    WebRequest wr = WebRequest.Create("http://pv.sohu.com/cityjson?ie=utf-8");
                    using (StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        ip = sr.ReadToEnd();
                        ip = ip.Substring(ip.IndexOf(":") + 1, 15).Replace("\"", "").Trim();
                    }
                }
                catch { }
                return ip;
            }
        }



 
        /// <summary>
        /// 随机中文码
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(ConstTool.constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
    }
    public class Properties
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 字段属性值
        /// </summary>
        public DisplayNameAttribute Attribute { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsKey { get; set; }
    }
}
