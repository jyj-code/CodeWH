using ProjectBase.Core.Util;
using System;
using System.Runtime.InteropServices;
using System.Text;
using WebCrawler;

namespace ConsoleApplication
{
    class Program
    {
        public static string getMemory(object o) // 获取引用类型的内存地址方法
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.Pinned);
            IntPtr addr = h.AddrOfPinnedObject();
            return "0x" + addr.ToString("X");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var ff = StringUtil.BarCode("jjjjhhhhhhhh",2,3,0);

            new WanDaInfoInterView().InterView();

            string a = "abcABC123@;',";
            var b = a;
            a = "1c";
            Console.WriteLine(string.Format("A:{0},B:{1}", a, b));
            int i1 = 123;
            var i2 = i1;
            i1 = 1;
            Console.WriteLine(string.Format("A:{0},B:{1}", i1, i2));
            //Crawler.Main();
            //StringBuilder str = new StringBuilder();
            //for (int i = 0; i < 1000; i++)
            //{
            //    str.Append(RandomString.GetRandomString(100));
            //}
            //var strS = str.ToString();
            //uint y = 1023456789;
            ////var x = GetUnit(y);
            //for (int i = 9; i >= 1; i--)
            //{
            //    for (int j = 1; j <= i; j++)
            //    {
            //        Console.Write(string.Format("{0}*{1}={2} ", j, i, j * i));
            //    }
            //    Console.WriteLine("");
            //}
            //var encrypt= MD5Helper.MD5Encrypt(strS);
            //var Decrypt = MD5Helper.MD5Decrypt(encrypt);
            //Convert.ToBase64String(Encoding.Unicode.GetBytes(strS));
            //string encrypt64 = Convert.ToBase64String(Encoding.Unicode.GetBytes(strS));
            //var a64 = Encoding.Unicode.GetString(Convert.FromBase64String(encrypt64));
            //Console.WriteLine(a64);
            Console.ReadKey();
            //Console.WriteLine(String.Format("加密字符长度{0}，解密正确性{1}",str.Length, strS == Decrypt));
        }
        public static uint GetUnit(uint n)
        {
            uint x = new uint();
            string result = n.ToString();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = result.Length; i > 0; i--)
            {
                var a = result.Substring(i - 1, 1);
                stringBuilder.Append(a);
            }
            x = Convert.ToUInt32(stringBuilder.ToString());
            return x;
        }
    }
}
