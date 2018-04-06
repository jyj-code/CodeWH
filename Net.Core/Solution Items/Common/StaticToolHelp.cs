using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class StaticToolHelp
    {
        public static string ConnectionDatabaseStr { get; set; }
        public static string ConnectionDatabaseType { get; set; }
        /// <summary>
        /// 字符串去除Html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");
            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);
            return strText;
        }
        public static string StringConvertGB2312(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
            //  return Encoding.GetEncoding("GB2312").GetString(Encoding.GetEncoding("GB2312").GetBytes(unicodeString));
        }
        /// <summary>
        /// 截取指定长度的字符串返回
        /// </summary>
        /// <param name="ContentStr">要截取的字符串</param>
        /// <param name="LengthNumber">要截取的长度，中文占2个字符，</param>
        /// <returns></returns>
        public static string GetSeparateSubString(string ContentStr, int LengthNumber)
        {
            List<string> resultList = new List<string>();
            string tempStr = ContentStr.Trim();
            int charNumber = LengthNumber;
            int totalCount = 0;
            string mSubStr = "";
            for (int i = 0; i < tempStr.Length; i++)
            {
                string mChar = tempStr.Substring(i, 1);
                int byteCount = Encoding.Unicode.GetByteCount(mChar);//关键点判断字符所占的字节数

                if (byteCount == 1)
                {
                    totalCount++;
                    mSubStr += mChar;
                    if (totalCount == charNumber || i == tempStr.Length - 1)
                    {
                        resultList.Add(mSubStr);
                        totalCount = 0;
                        mSubStr = "";
                    }
                }
                else if (byteCount > 1)
                {

                    totalCount += 2;
                    if (totalCount > charNumber)
                    {
                        resultList.Add(mSubStr);
                        if (i == tempStr.Length - 1)
                        {
                            mSubStr = mChar;
                            resultList.Add(mSubStr);
                        }
                        else
                        {
                            totalCount = 2;
                            mSubStr = mChar;
                        }
                    }
                    else if (totalCount == charNumber)
                    {
                        mSubStr += mChar;
                        resultList.Add(mSubStr);
                        totalCount = 0;
                        mSubStr = "";
                    }
                    else if (i == tempStr.Length - 1)
                    {
                        mSubStr += mChar;
                        resultList.Add(mSubStr);
                    }
                    else
                    {
                        mSubStr += mChar;
                    }
                }
                if (resultList.Count > 0)
                    break;
            }
            return resultList[0].ToString().Trim();
        }
        /// <summary>
        /// 判断对象是否为null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullVar(Object obj)
        {
            if (obj == null || obj.ToString() == "0" || string.IsNullOrEmpty(obj.ToString()) || obj.ToString() == "" || obj.ToString().Length < 1)
                return false;
            else
                return true;
        }
    }
}
