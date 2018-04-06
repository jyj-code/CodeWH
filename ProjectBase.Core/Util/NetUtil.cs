using System.Web;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ProjectBase.Core.Util
{
    public class NetUtil
    {
        /// <summary>
        /// 获取指定 指定URL的HTML源代码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">如果为NULL 则自动去获取编码</param>
        /// <returns></returns>
        public static string GetHtml(string url, Encoding encoding)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse res;

                try
                {
                    res = (HttpWebResponse)hwr.GetResponse();
                }
                catch
                {
                    return string.Empty;
                }

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream mystream = res.GetResponseStream())
                    {
                        //没有指定编码，猜
                        if (encoding == null)
                        {
                            return DecodeData(mystream, res);
                            //using (MemoryStream msTemp = new MemoryStream())
                            //{
                            //    int len = 0;
                            //    byte[] buff = new byte[512];

                            //    while ((len = mystream.Read(buff, 0, 512)) > 0)
                            //    {
                            //        msTemp.Write(buff, 0, len);

                            //    }
                            //    res.Close();

                            //    if (msTemp.Length > 0)
                            //    {
                            //        msTemp.Seek(0, SeekOrigin.Begin);

                            //        byte[] PageBytes = new byte[msTemp.Length];

                            //        msTemp.Read(PageBytes, 0, PageBytes.Length);
                            //        msTemp.Seek(0, SeekOrigin.Begin);

                            //        int DetLen = 0;
                            //        byte[] DetectBuff = new byte[4096];

                            //        CharsetListener listener = new CharsetListener();
                            //        UniversalDetector Det = new UniversalDetector(null);

                            //        while ((DetLen = msTemp.Read(DetectBuff, 0, DetectBuff.Length)) > 0 && !Det.IsDone())
                            //        {
                            //            Det.HandleData(DetectBuff, 0, DetectBuff.Length);
                            //        }

                            //        Det.DataEnd();

                            //        if (Det.GetDetectedCharset() != null)
                            //        {
                            //            return System.Text.Encoding.GetEncoding(Det.GetDetectedCharset()).GetString(PageBytes);
                            //        }
                            //    }
                            //}
                        }
                        //指定了编码
                        else
                        {
                            using (StreamReader reader = new StreamReader(mystream, encoding))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static string DecodeData(Stream responseStream, HttpWebResponse response)
        {
            string name = null;
            string text2 = response.Headers["content-type"];
            if (text2 != null)
            {
                int index = text2.IndexOf("charset=");
                if (index != -1)
                {
                    name = text2.Substring(index + 8);
                }
            }
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[0x400];
            for (int i = responseStream.Read(buffer, 0, buffer.Length); i > 0; i = responseStream.Read(buffer, 0, buffer.Length))
            {
                stream.Write(buffer, 0, i);
            }
            responseStream.Close();
            if (name == null)
            {
                MemoryStream stream3 = stream;
                stream3.Seek((long)0, SeekOrigin.Begin);
                string text3 = new StreamReader(stream3, Encoding.ASCII).ReadToEnd();
                if (text3 != null)
                {
                    int startIndex = text3.IndexOf("charset=");
                    int num4 = -1;
                    if (startIndex != -1)
                    {
                        num4 = text3.IndexOf("\"", startIndex);
                        if (num4 != -1)
                        {
                            int num5 = startIndex + 8;
                            name = text3.Substring(num5, (num4 - num5) + 1).TrimEnd(new char[] { '>', '"' });
                        }
                    }
                }
            }
            Encoding aSCII = null;
            if (name == null)
            {
                aSCII = Encoding.GetEncoding("gb2312");
            }
            else
            {
                try
                {
                    if (name == "GBK")
                    {
                        name = "GB2312";
                    }
                    aSCII = Encoding.GetEncoding(name);
                }
                catch
                {
                    aSCII = Encoding.GetEncoding("gb2312");
                }
            }
            stream.Seek((long)0, SeekOrigin.Begin);
            StreamReader reader2 = new StreamReader(stream, aSCII);
            return reader2.ReadToEnd();
        }
    }
}