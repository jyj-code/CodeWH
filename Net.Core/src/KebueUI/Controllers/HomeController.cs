using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using KebueUI.Models;
using Microsoft.Extensions.Caching.Memory;
using ProtoBuf;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace KebueUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult WelCome()
        {
            return View();
        }
        public IActionResult Whale()
        {
            return View();
        }
        public IActionResult HtmlRunTest()
        {
            List<ArticleType> ArticleType = _memoryCache.Get("ArticleType") as List<ArticleType>;
            if (ArticleType == null)
            {
                ArticleType = new B_ArticleType().Find();
                _memoryCache.Set("ArticleType", ArticleType);
            }
            ViewData["ArticleType"] = ArticleType;
            return View();
        }
        
        public IActionResult Index()
        {
          var d=  AutoGenerationHtml;
            #region 通知信息
            var Announcement = _memoryCache.Get("Announcement");
            if (Announcement == null)
            {
                Announcement = new B_Sys_Announcement().Find().First().AnnouncementContent;
                _memoryCache.Set<object>("Announcement", Announcement);
            }
            ViewData["Announcement"] = Announcement.ToString();
            #endregion
            #region 当前登录用户信息
            string CurrentLoginUser = GetSession("CurrentLoginUser");
            S_User user = new S_User();
            if (!string.IsNullOrEmpty(CurrentLoginUser))
            {
                user = new B_S_User().GetUserObj(CurrentLoginUser);
            }
            ViewData["CurrentLoginUser"] = user;
            #endregion
            #region 文章类型 ArticleType
            List<ArticleType> ArticleType = _memoryCache.Get("ArticleType") as List<ArticleType>;
            if (ArticleType == null)
            {
                ArticleType = new B_ArticleType().Find();
                _memoryCache.Set("ArticleType", ArticleType);
            }
            ViewData["ArticleType"] = ArticleType;
            #endregion
            //AutoGenerationHtml;
            #region 友情链接 Link
            List<Link> Link = _memoryCache.Get("Link") as List<Link>;
            if (Link == null)
            {
                Link = new B_Link().Find();
                _memoryCache.Set("Link", Link);
            }
            ViewData["Link"] = Link;
            #endregion


            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        private IMemoryCache _memoryCache;
        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpPost]
        public IActionResult AutoTemplateCreate(int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        HtmlHelper.AutoCreateArticle();

                        break;
                    case 2:
                        HtmlHelper.AutoCreateArticleType();
                        _memoryCache.Remove("ArticleType");//清除导航栏菜单缓存
                        break;
                }
            }
            catch (Exception ex)
            {
                return Ok("发生异常：" + ex.ToString());
            }
            return Ok("更新成功");
        }
        public IActionResult LoginOut()
        {
            try
            {
                _memoryCache.Remove("CurrentLoginUser");
            }
            catch (Exception ex)
            {
                return Ok("发生异常：" + ex.ToString());
            }
            return Ok("退出成功");
        }
        [HttpPost]
        public IActionResult LoginVerify(string Account, string Password)
        {
            var UserObj = new B_S_User().GetUserObj(Account);
            if (UserObj != null)
            {
                if (UserObj.Password == Password)
                {
                    HttpContext.Session.SetString("CurrentLoginUser", UserObj.UserAccount);
                    return Ok("登录成功");
                }
            }
            return Ok("0");
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        /// <summary>
        /// 模板自动生成并清楚缓存
        /// </summary>
        public bool AutoGenerationHtml
        {
            get
            {
                try
                {
                    HtmlHelper.AutoCreateArticle();
                    HtmlHelper.AutoCreateArticleType();
                    _memoryCache.Remove("ArticleType");
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSession(string key)
        {
            try
            {
                return HttpContext.Session.GetString(key);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    // [ProtoMember]
    [ProtoContract]
    public class User
    {
        public string ID { get; set; }
        public string UserAccount { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime Registration_Date { get; set; }
        public string Role_ID { get; set; }
        public string Nation { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string Actual_name { get; set; }
        public string State_license_number { get; set; }
        public string Photo { get; set; }
        public string Image_Log { get; set; }
        public string Link_address { get; set; }
        public string Template_ID { get; set; }
    }
}
