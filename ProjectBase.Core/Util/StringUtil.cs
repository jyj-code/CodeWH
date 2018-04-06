using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ProjectBase.Core.Util
{
    #region 将字符串以行为单位进行分割的正则
    /// <summary>
    /// 将字符串以行为单位进行分割的正则
    /// </summary>
    public class SplitLineRegex : Regex
    {
        private const string PATTERN = @"\r\n|\r|\n";

        public SplitLineRegex()
            : base(PATTERN)
        { }
    }
    #endregion

    #region 尖扩号
    /// <summary>
    /// 尖扩号
    /// </summary>
    public class AngleBracketRegex : Regex
    {
        private const string PATTERN = @"<.*?>";

        private const RegexOptions OPTIONS = RegexOptions.IgnoreCase | RegexOptions.Singleline;

        public AngleBracketRegex()
            : base(PATTERN, OPTIONS)
        {
        }
    }
    #endregion

    #region ImageRegex
    public class ImageRegex : Regex
    {
        //此正则不够用...TODO...
        private const string PATTERN = @"<img\x20+src=""(?<src>.+?)"".*?>";

        private const RegexOptions OPTIONS = RegexOptions.IgnoreCase;

        public ImageRegex()
            : base(PATTERN, OPTIONS)
        {
        }

    }
    #endregion

    #region EmoticonRegex
    public class EmoticonRegex : Regex
    {
        private const string PATTERN = @"<img\s+src="".+?""\s+emoticon=""(.+?)""[^>]*>";

        private const RegexOptions OPTIONS = RegexOptions.IgnoreCase | RegexOptions.Compiled;

        public EmoticonRegex()
            : base(PATTERN, OPTIONS)
        {

        }
    } 
    #endregion

    public delegate int ForEachAction<T>(int index, T value);

    /// <summary>
    /// 字符串助手类
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 生成条形码：如：bar_code("20070520122334", 20, 1, 1);
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch">度度</param>
        /// <param name="cw">线条宽度</param>
        /// <param name="type_code">是否输出文字1为输出</param>
        /// <returns></returns>
        public static string BarCode(object str, int ch, int cw, int type_code)
        {
            #region
            string strTmp = str.ToString();
            string code = strTmp;
            strTmp = strTmp.ToLower();
            int height = ch;
            int width = cw;

            strTmp = strTmp.Replace("0", "_|_|__||_||_|"); ;
            strTmp = strTmp.Replace("1", "_||_|__|_|_||");
            strTmp = strTmp.Replace("2", "_|_||__|_|_||");
            strTmp = strTmp.Replace("3", "_||_||__|_|_|");
            strTmp = strTmp.Replace("4", "_|_|__||_|_||");
            strTmp = strTmp.Replace("5", "_||_|__||_|_|");
            strTmp = strTmp.Replace("7", "_|_|__|_||_||");
            strTmp = strTmp.Replace("6", "_|_||__||_|_|");
            strTmp = strTmp.Replace("8", "_||_|__|_||_|");
            strTmp = strTmp.Replace("9", "_|_||__|_||_|");
            strTmp = strTmp.Replace("a", "_||_|_|__|_||");
            strTmp = strTmp.Replace("b", "_|_||_|__|_||");
            strTmp = strTmp.Replace("c", "_||_||_|__|_|");
            strTmp = strTmp.Replace("d", "_|_|_||__|_||");
            strTmp = strTmp.Replace("e", "_||_|_||__|_|");
            strTmp = strTmp.Replace("f", "_|_||_||__|_|");
            strTmp = strTmp.Replace("g", "_|_|_|__||_||");
            strTmp = strTmp.Replace("h", "_||_|_|__||_|");
            strTmp = strTmp.Replace("i", "_|_||_|__||_|");
            strTmp = strTmp.Replace("j", "_|_|_||__||_|");
            strTmp = strTmp.Replace("k", "_||_|_|_|__||");
            strTmp = strTmp.Replace("l", "_|_||_|_|__||");
            strTmp = strTmp.Replace("m", "_||_||_|_|__|");
            strTmp = strTmp.Replace("n", "_|_|_||_|__||");
            strTmp = strTmp.Replace("o", "_||_|_||_|__|");
            strTmp = strTmp.Replace("p", "_|_||_||_|__|");
            strTmp = strTmp.Replace("r", "_||_|_|_||__|");
            strTmp = strTmp.Replace("q", "_|_|_|_||__||");
            strTmp = strTmp.Replace("s", "_|_||_|_||__|");
            strTmp = strTmp.Replace("t", "_|_|_||_||__|");
            strTmp = strTmp.Replace("u", "_||__|_|_|_||");
            strTmp = strTmp.Replace("v", "_|__||_|_|_||");
            strTmp = strTmp.Replace("w", "_||__||_|_|_|");
            strTmp = strTmp.Replace("x", "_|__|_||_|_||");
            strTmp = strTmp.Replace("y", "_||__|_||_|_|");
            strTmp = strTmp.Replace("z", "_|__||_||_|_|");
            strTmp = strTmp.Replace("-", "_|__|_|_||_||");
            strTmp = strTmp.Replace("*", "_|__|_||_||_|");
            strTmp = strTmp.Replace("/", "_|__|__|_|__|");
            strTmp = strTmp.Replace("%", "_|_|__|__|__|");
            strTmp = strTmp.Replace("+", "_|__|_|__|__|");
            strTmp = strTmp.Replace(".", "_||__|_|_||_|");
            strTmp = strTmp.Replace("_", "<span   style='height:" + height + ";width:" + width + ";background:#FFFFFF;'></span>");
            strTmp = strTmp.Replace("|", "<span   style='height:" + height + ";width:" + width + ";background:#000000;'></span>");

            if (type_code == 1)
            {
                return strTmp + "<BR>" + code;
            }
            else
            {
                return strTmp;
            }
            #endregion
        }




        private static SplitLineRegex splitLineRegex;
        private static AngleBracketRegex angleBracketRegex;
        private static Regex booleanFormatRegex;
        private static ImageRegex imageBracketRegex;
        private static EmoticonRegex emoticonBracketRegex;


        public static string Replace(string text, string oldValue, string newValue)
        {
            return Regex.Replace(text, oldValue, newValue, RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// 打包一个json对象(实用于简单的类型) 
        /// </summary>
        /// <param name="jsonPropertys"></param>
        /// <returns></returns>
        public static string BuildJsonObject(Dictionary<string, object> jsonPropertys)
        {
            StringBuffer sb = new StringBuffer("{");
            Dictionary<string, object>.Enumerator em = jsonPropertys.GetEnumerator();
            bool isNumber = false;
            while (em.MoveNext())
            {
                isNumber = em.Current.Value is short
                    || em.Current.Value is int
                    || em.Current.Value is long
                    || em.Current.Value is byte
                    || em.Current.Value is float
                    || em.Current.Value is double
                    || em.Current.Value is decimal;
                sb += em.Current.Key + ":";
                if (!isNumber) sb += "\"";
                if (!isNumber)
                    sb += ToJavaScriptString(em.Current.Value.ToString());
                else
                    sb += em.Current.Value;
                if (!isNumber) sb += "\"";
                sb += ",";
            }
            if (sb.Length > 1) sb.Remove(sb.Length - 1, 1);
            sb += "}";
            return sb.ToString();
        }

        public static string BuildJsonObject(object obj)
        {
            return JsonBuilder.GetJson(obj, null);
        }

        public static string BuildJsonObject(object obj, params string[] excludePropertys)
        {
            return JsonBuilder.GetJson(obj, excludePropertys);
        }


        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuiderRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder buider = new StringBuilder(length);

            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                buider.Append(chars[(int)rnd.Next(0, chars.Length)]);
            }
            return buider.ToString();
        }

        /// <summary>
        /// 判断一个字符串是否是有效的布尔值
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsBooleanFormat(string text)
        {
            if (booleanFormatRegex == null)
                booleanFormatRegex = new Regex("^(true|false)$", RegexOptions.IgnoreCase);
            return booleanFormatRegex.IsMatch(text);
        }

        /// <summary>
        /// 判断一个字符串是否是有效的整数值
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsIntegerFormat(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            int totalChars = text.Length;
            for (int i = 0; i < totalChars; i++)
            {
                if (char.IsNumber(text[i]) == false)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 将字符串分行
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] GetLines(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new string[0];

            if (splitLineRegex == null)
                splitLineRegex = new SplitLineRegex();

            return splitLineRegex.Split(text);
        }

        /// <summary>
        /// string转int
        /// </summary>
        /// <param name="defaultValue">转换失败,返回此值</param>
        public static int GetInt(string input, int defaultValue)
        {
            int result;
            if (!int.TryParse(input, out result))
                result = defaultValue;
            return result;
        }

        /// <summary>
        /// 将一个字符串转为SQL字符串的内容。
        /// 注意返回结果不包含SQL字符串的开始和结束部分的单引号。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToSqlString(string text)
        {
            return text.Replace("'", "''");
        }

        /// <summary>
        /// 转到JS用的string
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string ToJavaScriptString(string text)
        {
            StringBuffer buffer = new StringBuffer(text);
            buffer.Replace("\\", @"\\");
            buffer.Replace("\t", @"\t");
            buffer.Replace("\n", @"\n");
            buffer.Replace("\r", @"\r");
            buffer.Replace("\"", @"\""");
            buffer.Replace("\'", @"\'");
            buffer.Replace("/", @"\/");
            return buffer.ToString();
        }

        /// <summary>
        /// 循环字符串中的每一行
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="action">循环时所要执行的动作，循环过程中执行动作后返回0时，表示退出循环</param>
        public static void ForEachLines(string text, ForEachAction<string> action)
        {
            string[] lines = GetLines(text);

            for (int i = 0; i < lines.Length; i++)
            {
                int result = action(i, lines[i]);

                //当动作返回0时，则退出循环
                if (result == 0)
                    return;
            }
        }
        /// <summary>
        /// 将字符串列表按逗号分隔符合并
        /// </summary>
        /// <param name="array">所要合并的字符串列表</param>
        /// <returns>合并结果</returns>
        public static string Join(IEnumerable array)
        {
            return string.Join(",", array);
        }

        /// <summary>
        /// 将字符串按,分割，并返回int类型的数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] Split(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new string[0];
            return input.Split(',');
        }

        /// <summary>
        /// 将字符串按固定分隔符分割，并返回int类型的数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] Split(string input, char separator)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.Split(separator);
            }
            return new string[0];
        }
        private static Encoding s_EncodingCache = null;

        /// <summary>
        /// 尝试获取GB2312编码并缓存起来，如果运行环境不支持GB2312编码，将缓存系统默认编码
        /// </summary>
        private static Encoding EncodingCache
        {
            get
            {
                if (s_EncodingCache == null)
                {

                    try
                    {
                        s_EncodingCache = Encoding.GetEncoding(936);

                    }
                    catch { }

                    if (s_EncodingCache == null)
                        s_EncodingCache = Encoding.UTF8;

                }

                return s_EncodingCache;
            }
        }

        /// <summary>
        /// 获取字符串的字节长度，默认自动尝试用GB2312编码获取，
        /// 如果当前运行环境支持GB2312编码，英文字母将被按1字节计算，中文字符将被按2字节计算
        /// 如果尝试使用GB2312编码失败，将采用当前系统的默认编码，此时得到的字节长度根据具体运行环境默认编码而定
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>字符串的字节长度</returns>
        public static int GetByteCount(string text)
        {
            return EncodingCache.GetByteCount(text);
        }

        /// <summary>
        /// 计算行号
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="endIndex">结束位置</param>
        /// <returns></returns>
        public static int LineCount(string text, int startIndex, int endIndex)
        {
            int num = 0;

            while (startIndex < endIndex)
            {
                if ((text[startIndex] == '\r') || ((text[startIndex] == '\n') && ((startIndex == 0) || (text[startIndex - 1] != '\r'))))
                {
                    num++;
                }

                startIndex++;
            }

            return num;
        }

        /// <summary>
        /// 忽略大小写的字符串比较
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
            {
                return true;
            }

            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                return false;
            }

            if (s2.Length != s1.Length)
            {
                return false;
            }

            return (0 == string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase));
        }


        public static bool StartsWith(string text, char lookfor)
        {
            return (text.Length > 0 && text[0] == lookfor);
        }

        /// <summary>
        /// 快速判断字符串起始部分
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool StartsWith(string target, string lookfor)
        {
            if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(lookfor))
            {
                return false;
            }

            if (lookfor.Length > target.Length)
            {
                return false;
            }

            return (0 == string.Compare(target, 0, lookfor, 0, lookfor.Length, StringComparison.Ordinal));
        }

        /// <summary>
        /// 快速判断字符串起始部分
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(string target, string lookfor)
        {
            if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(lookfor))
            {
                return false;
            }

            if (lookfor.Length > target.Length)
            {
                return false;
            }
            return (0 == string.Compare(target, 0, lookfor, 0, lookfor.Length, StringComparison.OrdinalIgnoreCase));
        }

        public static bool EndsWith(string text, char lookfor)
        {
            return (text.Length > 0 && text[text.Length - 1] == lookfor);
        }

        public static bool EndsWith(string target, string lookfor)
        {
            int indexA = target.Length - lookfor.Length;

            if (indexA < 0)
            {
                return false;
            }

            return (0 == string.Compare(target, indexA, lookfor, 0, lookfor.Length, StringComparison.Ordinal));
        }

        /// <summary>
        /// 快递判断字符串结束部分
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(string target, string lookfor)
        {
            int indexA = target.Length - lookfor.Length;

            if (indexA < 0)
            {
                return false;
            }

            return (0 == string.Compare(target, indexA, lookfor, 0, lookfor.Length, StringComparison.OrdinalIgnoreCase));
        }

        public static bool Contains(string target, string lookfor)
        {
            if (target.Length < lookfor.Length)
                return false;

            return (0 <= target.IndexOf(lookfor));
        }

        /// <summary>
        /// 忽略大小写判断字符串是否包含
        /// </summary>
        /// <param name="target"></param>
        /// <param name="lookfor"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(string target, string lookfor)
        {
            if (target.Length < lookfor.Length)
                return false;

            return (0 <= target.IndexOf(lookfor, StringComparison.OrdinalIgnoreCase));
        }


        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(string text, int length)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (length < 1)
                return text;

            byte[] buf = EncodingCache.GetBytes(text);

            if (buf.Length <= length)
            {
                return text;
            }

            int newLength = length;
            int[] numArray1 = new int[length];
            byte[] newBuf = null;
            int counter = 0;
            for (int i = 0; i < length; i++)
            {
                if (buf[i] > 0x7f)
                {
                    counter++;
                    if (counter == 3)
                    {
                        counter = 1;
                    }
                }
                else
                {
                    counter = 0;
                }
                numArray1[i] = counter;
            }

            if ((buf[length - 1] > 0x7f) && (numArray1[length - 1] == 1))
            {
                newLength = length + 1;
            }
            newBuf = new byte[newLength];
            Array.Copy(buf, newBuf, newLength);
            return EncodingCache.GetString(newBuf) + "...";

        }

        public static int FirstIndexOf(string source, int startIndex, int length, out string match, params string[] lookfors)
        {
            int index = -1;
            int itemIndex = -1;

            for (int i = 0; i < lookfors.Length; i++)
            {
                int temp = source.IndexOf(lookfors[i], startIndex, length);

                if (index < 0 || (temp >= 0 && temp < index))
                {
                    index = temp;
                    itemIndex = i;
                }
            }

            if (itemIndex >= 0)
                match = lookfors[itemIndex];
            else
                match = null;

            return index;
        }

        /// <summary>
        /// 友好大小
        /// </summary>
        public static string FriendlyCapacitySize(long value)
        {
            if (value < 1024 * 5 && value % 1024 != 0)
            {
                return value + " B";
            }
            else if (value < 1024 * 5 && value % 1024 == 0)
            {
                return (value / 1024) + " KB";
            }
            else if (value >= 1024 * 5 && value < 1024 * 1024)
            {
                return (value / 1024) + " KB";
            }
            else if (value < 1024 * 1024 * 5 && value % (1024 * 1024) != 0)
            {
                return (value / 1024) + " KB";
            }
            else if (value < 1024 * 1024 * 5 && value % (1024 * 1024) == 0)
            {
                return (value / (1024 * 1024)) + " MB";
            }
            else if (value >= 1024 * 1024 * 5 && value < 1024 * 1024 * 1024)
            {
                return (value / (1024 * 1024)) + " MB";
            }
            else
            {
                return (value / (1024 * 1024 * 1024)) + " GB";
            }
        }

        public static string GetSafeFormText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            StringBuilder result = new StringBuilder(text);
            result.Replace("\"", "&quot;");
            result.Replace("<", "&lt;");
            result.Replace(">", "&gt;");

            return result.ToString();
        }

        private static Regex scriptReg = null;

        /// <summary>
        /// 去除尖括号以及其中的内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ClearAngleBracket(string content) { return ClearAngleBracket(content, false); }
        public static string ClearAngleBracket(string content, bool processImages)
        {
            if (content == null || content.Length == 0)
                return content;

            if (processImages)
            {
                if (imageBracketRegex == null)
                    imageBracketRegex = new ImageRegex();
                if (emoticonBracketRegex == null)
                    emoticonBracketRegex = new EmoticonRegex();

                content = imageBracketRegex.Replace(content, "[表情]");
                content = emoticonBracketRegex.Replace(content, "[图片]");
            }

            if (angleBracketRegex == null)
                angleBracketRegex = new AngleBracketRegex();

            return angleBracketRegex.Replace(content, string.Empty);
        }

        /// <summary>
        /// 清除末尾的换行和空格(性能差 用于发表的时候)
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ClearEndLineFeedAndBlank(string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;
            content = Regex.Replace(content, "<br>", "<br />", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br/>", "<br />", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br />", "<br />", RegexOptions.IgnoreCase);//主要作用是把大写转为小写 如"<Br />" 转成 "<br />"
            content = content.Replace("\n", "<br />");
            content = content.Replace("\r\n", "<br />");

            string[] contents = Regex.Split(content, "<br />");
            if (contents.Length > 1)
                content = ClearEndLineFeedAndBlank(contents, "<br />");

            //contents = content.Split('\n');

            //content = a(contents, "\n");


            return content.TrimEnd();
        }

        private static string ClearEndLineFeedAndBlank(string[] contents, string spliter)
        {
            StringBuilder result = new StringBuilder();

            bool hasContent = false;
            for (int i = contents.Length - 1; i > -1; i--)
            {
                if (hasContent == false)
                {
                    string temp = contents[i].Replace("&nbsp;", " ");

                    if (temp.Trim() != string.Empty)
                    {
                        result.Insert(0, contents[i].TrimEnd().Replace(" ", "&nbsp;"));
                        hasContent = true;
                    }
                }
                else
                {
                    result.Insert(0, spliter);
                    result.Insert(0, contents[i]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 如果内容只有换行和空格  返回空字符串   否则返回原内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Trim(string content)
        {
            if (string.IsNullOrEmpty(content) == false)
            {
                string tempContent = Regex.Replace(content, "(<br />)|(<br>)|(<br/>)|(&nbsp;)", string.Empty, RegexOptions.IgnoreCase);
                if (tempContent == string.Empty)
                    return string.Empty;
                else
                    return content;
            }

            return content;
        }


        //static readonly string[] excludeHtmlTags = new string[] { "body", "frame", "frameset", "html", "iframe", "style", "ilayer", "layer", "link", "meta", "applet", "form", "input", "select", "textarea" };//, "embed", "object","script"};


        ///// <summary>
        ///// 过滤HTML标签
        ///// </summary>
        ///// <param name="html"></param>
        ///// <returns></returns>
        //public static string ConvertHtmlToSafety(string html)
        //{
        //    string pattern="";
        //    html = FilterScript(html);//脚本
        //    foreach (string s in excludeHtmlTags)
        //    {
        //        pattern=string.Format("</?{0}.*?/?>",s);
        //        if (Regex.IsMatch(html, pattern, RegexOptions.IgnoreCase))
        //        {
        //            html = Regex.Replace(html, pattern, string.Empty, RegexOptions.IgnoreCase);
        //        }
        //    }
        //    return html;
        //}

        /// <summary>
        /// 对字符串进行Html解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HtmlDecode(string content)
        {
            return HttpUtility.HtmlDecode(content);
        }

        /// <summary>
        /// 对字符串进行Html编码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HtmlEncode(string content)
        {
            return HttpUtility.HtmlEncode(content);
        }


        /// <summary>
        /// <函数：Decode>
        ///作用：将16进制数据编码转化为字符串，是Encode的逆过程
        /// </summary>
        /// <param name="strDecode"></param>
        /// <returns></returns>
        public static string HexDecode(string strDecode)
        {
            if (strDecode.IndexOf(@"\u") == -1)
                return strDecode;

            int startIndex = 0;
            if (strDecode.StartsWith(@"\u") == false)
            {
                startIndex = 1;
            }

            string[] codes = Regex.Split(strDecode, @"\\u");

            StringBuilder result = new StringBuilder();
            if (startIndex == 1)
                result.Append(codes[0]);
            for (int i = startIndex; i < codes.Length; i++)
            {
                try
                {
                    if (codes[i].Length > 4)
                    {
                        result.Append((char)short.Parse(codes[i].Substring(0, 4), global::System.Globalization.NumberStyles.HexNumber));
                        result.Append(codes[i].Substring(4));
                    }
                    else
                    {
                        result.Append((char)short.Parse(codes[i].Substring(0, 4), global::System.Globalization.NumberStyles.HexNumber));
                    }
                }
                catch
                {
                    result.Append(codes[i]);
                }
            }

            return result.ToString();
        }

    }
}
