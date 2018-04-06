using ILove.Models;
using Log4net;
using NET.Architect.Model;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace ILove.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult DemoTest()
        {
            return View();
        }
        public ActionResult Love()
        {
            ViewBag.ProductionDate = ConfigurationSettings.AppSettings["ProductionDate"].ToString();
            var PeopleBy = ConfigurationSettings.AppSettings["PeopleBy"].ToString();
            ViewBag.PeopleBy = PeopleBy;
            var SendBy = ConfigurationSettings.AppSettings["SendBy"].ToString();
            ViewBag.SendBy = SendBy;
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Title = $"{PeopleBy}求爱{SendBy}现场";
            return View();
        }
        public ActionResult LoveTree()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserInfo(CustomerUserInfo data, string ip, string modelType)
        {
            var Flage = Request.Cookies["userInfoFlage"];
            Response.Cookies["userInfoFlage"].Expires = DateTime.Now.AddHours(1);
            try
            {
                if (Flage == null)
                    UserInfoMethod(data, ip, modelType);
                else
                {
                    if (Flage.Value != $"{data.Ip}{ip}")
                        UserInfoMethod(data, ip, modelType);
                }
            }
            catch { }
            return this.Json(null, JsonRequestBehavior.AllowGet);
        }
        public void UserInfoMethod(CustomerUserInfo data, string ip, string modelType)
        {
            StringBuilder str = new StringBuilder();
            str.Append($"访客设备：{modelType}");
            str.Append(Environment.NewLine);
            str.Append(data.Ip + "    ");
            str.Append(data.Isp + "    ");
            str.Append(data.OS + "    ");
            str.Append(data.Browser + "    ");
            str.Append(Environment.NewLine);
            if (modelType != "md" || modelType != "pc")
                modelType = "pc";
            str.Append(ObjConvertStr(BaidHelghtIp(ip, modelType), ip));
            LoggerHelper.Info(str.ToString());
            Response.Cookies["userInfoFlage"].Value = $"{data.Ip}{ip}";
        }
        private string ObjConvertStr(BaiduHelghtIPEntity entity, string ip)
        {
            string enplty = "   ";
            StringBuilder str = new StringBuilder();
            if (entity.result.error == "161" && entity.content != null)
            {
                if (ip != null)
                    str.Append($"定位IP:{ip}" + enplty);
                if (entity.content.radius != null)
                    str.Append($"定位结果半径:{entity.content.radius}" + enplty);
                if (entity.content.confidence != null)
                    str.Append($"定位结果可信度:{entity.content.confidence}" + enplty);
                if (entity.content.location != null)
                    str.Append($"纬度坐标:{entity.content.location.lat}{enplty}经度坐标:{entity.content.location.lng}" + enplty);
                if (entity.content.address_component != null)
                {
                    str.Append(Environment.NewLine);
                    str.Append($"国家:{entity.content.address_component.country + enplty}省份:{entity.content.address_component.province + enplty}城市:{entity.content.address_component.city + enplty}区县:{entity.content.address_component.district + enplty}街道:{entity.content.address_component.street}" + Environment.NewLine);
                    if (!string.IsNullOrEmpty(entity.content.address_component.street_number))
                        str.Append($"门牌号:{entity.content.address_component.street_number}" + Environment.NewLine);
                    if (!string.IsNullOrEmpty(entity.content.address_component.street_number))
                        str.Append($"行政区划代码（身份证前6位):{entity.content.address_component.admin_area_code}" + enplty);
                }
                if (entity.content.formatted_address != null)
                    str.Append($"结构化地址信息:{entity.content.formatted_address}" + Environment.NewLine);
                if (entity.content.Pois != null)
                {
                    str.Append("pois（1000m以内的最多10条poi：");
                    str.Append($"名称:{entity.content.Pois.name}POI唯一标识ID:{entity.content.Pois.uid}地址:{entity.content.Pois.address}分类:{entity.content.Pois.tag}" + Environment.NewLine);
                    str.Append($"纬度(lat):{entity.content.Pois.location.lat} 经度(lng):{entity.content.Pois.location.lng}" + Environment.NewLine);
                }
                if (entity.content.locid != null)
                    str.Append($"唯一ID:{entity.content.locid}" + Environment.NewLine);
                if (entity.result.loc_time != null)
                    str.Append($"定位时间:{entity.result.loc_time}" + Environment.NewLine);
            }
            return str.ToString();
        }
        private BaiduHelghtIPEntity BaidHelghtIp(string LocationIP, string ModelType)
        {
            string strURL = $"http://api.map.baidu.com/highacciploc/v1?qcip={LocationIP}&qterm={ModelType}&ak=bDSpgtzrQvEEhAo1NdEEHjWo7f2KTiTn&extensions=3";
            System.Net.HttpWebRequest request;
            // 创建一个HTTP请求  
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var responseText = myreader.ReadToEnd();
            myreader.Close();
            return JsonConvert.DeserializeObject<BaiduHelghtIPEntity>(responseText);
        }






        public ActionResult Stars()
        {
            return View();
        }
        public ActionResult ind()
        {
            return View();
        }
        /// <summary>
        /// 时钟
        /// </summary>
        /// <returns></returns>
        public ActionResult Clock()
        {
            return View();
        }
        /// <summary>
        /// 钢琴
        /// </summary>
        /// <returns></returns>
        public ActionResult Piano()
        {
            return View();
        }
        /// <summary>
        /// 实现的人跑步动画
        /// </summary>
        /// <returns></returns>
        public ActionResult PeopleFlashRun()
        {
            return View();
        }
        /// <summary>
        /// 栩栩如生的星系
        /// </summary>
        /// <returns></returns>
        public ActionResult Galaxy()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Whale()
        {
            return View();
        }
    }
}