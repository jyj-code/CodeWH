using System.Configuration;

namespace ProjectBase.Core
{
    public class StaticParameters
    {
        public const bool proxyEnable = false;//是否启用上网代理，True为启用，False为不启用
        public const string proxyServer = "192.168.17.2";//代理服务器名称或者IP地址
        public static int proxyPort = 808;//代理服务器端口
        public static string GrabberPath = @"C:\kebue\";
        public const string UserAgent = "Mozilla/4.0   (compatible;   MSIE   6.0;   Windows   NT   5.1;   SV1;   .NET   CLR  2.0.50727) "; //根据CLR版本和NT版本适当修改。
        public const string CmdPath = @"C:\Windows\System32\cmd.exe";
    }
}
