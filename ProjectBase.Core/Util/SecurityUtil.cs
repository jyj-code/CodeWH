using System;
using System.IO;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web;

namespace ProjectBase.Core.Util
{
    public class SecurityUtil
    {
        public SecurityUtil()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static string SHA1(string text)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(text, "SHA1");
        }

        const string KEY_64 = "!bbsmax!";
        const string IV_64 = "!zzbird!"; //注意了，是8个字符，64位 

        public static string DesEncode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

        }

        public static string DesDecode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        public static string Base64Encode(string Message)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Message));
        }
        public static string Base64Decode(string Message)
        {
            byte[] outbyte = Convert.FromBase64String(Message);
            return System.Text.Encoding.UTF8.GetString(outbyte);
        }
    }
    /// <summary>
    /// 密码加密方式
    /// </summary>
    public enum EncryptFormat : byte
    {
        /// <summary>
        /// 明文
        /// </summary>
        ClearText = 0,
        /// <summary>
        /// MD5加密
        /// </summary>
        MD5 = 1,
        /// <summary>
        /// SHA1加密
        /// </summary>
        SHA1 = 2,
        /// <summary>
        /// bbsMax专用格式(客户端MD5+服务器端MD5)
        /// </summary>
        bbsMax = 3,
        /// <summary>
        /// 动网论坛格式
        /// </summary>
        Dvbbs = 4,
        /// <summary>
        /// 印象论坛格式
        /// </summary>
        NowBoard = 5,
        /// <summary>
        /// 魔力论坛格式 
        /// </summary>
        MolyX = 6,
        /// <summary>
        /// DZ6.1的加密格式
        /// </summary>
        Discuz = 7,
    }
}