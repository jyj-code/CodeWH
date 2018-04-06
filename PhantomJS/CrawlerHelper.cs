using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.IO;
using System.Threading;

namespace PhantomJS
{
    public class CrawlerHelper
    {
        /// <summary>
        /// NUGET安装
        ///     1.安装Selenium.WebDriver包
        ///     2.安装Selenium.PhantomJS.WebDriver包
        /// </summary>
        /// <param name="args"></param>
        public static string PhantomJs(string url, ref string path)
        {
            lock (typeof(CrawlerHelper))
            {
                string directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    string _pathOld = string.Format("{0}/{1}", directory.Substring(0, directory.LastIndexOf("\\")), DateTime.Now.AddDays(-60).ToString("yyMM"));
                    if (Directory.Exists(_pathOld))
                        Directory.Delete(_pathOld);
                }
                PhantomJSDriver driver = new PhantomJSDriver(GetPhantomJSDriverService());
                driver.Navigate().GoToUrl(url);
                driver.GetScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);
                return driver.PageSource;
            }
        }
        private readonly static object objectMinitor = new object();
        private static PhantomJSDriverService GetPhantomJSDriverService()
        {
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
            //service.GhostDriverPath = @"path\to\ghostdriver\main.js";
            //设置代理服务器地址            
            //pds.Proxy = $"{ip}:{port}"; 
            //设置代理服务器认证信息            
            //pds.ProxyAuthentication = GetProxyAuthorization();
            return service;
        }



        #region 模拟登录博客园
        static void Main(string[] args)
        {
            var url = "https://passport.cnblogs.com/user/signin";
            var driver1 = new PhantomJSDriver(GetPhantomJSDriverService2());
            driver1.Navigate().GoToUrl(url);

            if (driver1.Title == "用户登录 - 博客园")
            {
                driver1.FindElement(By.Id("input1")).SendKeys("xielongbao");
                driver1.FindElement(By.Id("input2")).SendKeys("1234");
                driver1.FindElement(By.Id("signin")).Click();
            }
            driver1.GetScreenshot().SaveAsFile(@"C:\aa.png", ScreenshotImageFormat.Png);
            var o = driver1.ExecuteScript("$('#signin').val('dsa')");

            Console.WriteLine(driver1.PageSource);
            driver1.Navigate().GoToUrl(url);
            Console.WriteLine(driver1.PageSource);
            IWebDriver driver2 = new PhantomJSDriver(GetPhantomJSDriverService2());
            driver2.Navigate().GoToUrl("https://home.cnblogs.com/");

            Console.WriteLine(driver2.PageSource);
            Console.WriteLine(driver1.PageSource);

            Console.Read();
        }

        private static PhantomJSDriverService GetPhantomJSDriverService2()
        {
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
            //Proxy proxy = new Proxy();
            //proxy.HttpProxy = string.Format("127.0.0.1:9999");

            //service.ProxyType = "http";
            //service.Proxy = proxy.HttpProxy;
            return service;
        } 
        #endregion
    }
}
