using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NET.Web.UI.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        //public ActionResult Index()
        //{
        //    Session["userid"] = Guid.NewGuid().ToString().ToUpper();
        //    Session["username"] = "网三";
        //    return View();
        //}
        /// <summary>
        /// 产生随机用户名函数
        /// </summary>
        /// <param name="length">用户名长度</param>
        /// <returns></returns>
        private static string GenerateRandomName(int length)
        {
            var newRandom = new System.Text.StringBuilder(62);
            var rd = new Random(DateTime.Now.Millisecond);
            for (var i = 0; i < length; i++)
            {
                newRandom.Append(Constant[rd.Next(62)]);
            }

            return newRandom.ToString();
        }
        private static readonly char[] Constant =
        {
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                    'w', 'x', 'y', 'z',
                    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
                    'W', 'X', 'Y', 'Z'
          };
    }
}