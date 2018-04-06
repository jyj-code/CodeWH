namespace KebueUI.Models
{
    public class CommonHelp
    {
        public static string modelType { get; set; }
        /// <summary>
        /// UserAgent解析系统
        /// </summary>
        /// <param name="userAgent">userAgent</param>
        /// <param name="modelType">设备类型</param>
        /// <returns></returns>
        public static string GetOSNameByUserAgent(string userAgent)
        {
            #region 手机
            modelType = "Mobile";
            if (userAgent.Contains("Android"))
            {
                return "Android";
            }
            else if (userAgent.Contains("iPhone"))
            {
                return "iPhone";
            }
            else if (userAgent.Contains("SymbianOS"))
            {
                return "SymbianOS";
            }
            else if (userAgent.Contains("Windows Phone"))
            {
                return "Windows Phone";
            }
            else if (userAgent.Contains("iPad"))
            {
                return "iPad";
            }
            else if (userAgent.Contains("iPod"))
            {
                return "iPod";
            }
            #endregion
            #region 电脑   
            else if (userAgent.Contains("NT 10.0"))
            {
                modelType = "PC";
                return "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                modelType = "PC";
                return "Windows 8.1";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                modelType = "PC";
                return "Windows 8";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                modelType = "PC";
                return "Windows 7";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                modelType = "PC";
                return "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                modelType = "PC";
                return "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                modelType = "PC";
                if (userAgent.Contains("64"))
                    return "Windows XP";
                else
                    return "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                modelType = "PC";
                return "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                modelType = "PC";
                return "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                modelType = "PC";
                return "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                modelType = "PC";
                return "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                modelType = "PC";
                return "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                modelType = "PC";
                return "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                modelType = "PC";
                return "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                modelType = "PC";
                return "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                modelType = "PC";
                return "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                modelType = "PC";
                return "SunOS";
            }
            else
            {
                modelType = "PC";
                return "";
            }
            #endregion
        }
    }
}
