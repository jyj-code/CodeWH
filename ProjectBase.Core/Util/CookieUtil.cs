using System;
using System.Web;

namespace ProjectBase.Core.Util
{
    public class CookieUtil
    {
        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <returns></returns>
        public static HttpCookie Get(string cookieKey)
        {
            return HttpContext.Current.Request.Cookies[cookieKey];
        }
    }
}