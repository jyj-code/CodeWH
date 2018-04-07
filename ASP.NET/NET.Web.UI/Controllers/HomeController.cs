using APIRequestServices;
using DotNet.Utilities;
using Log4net;
using NET.Architect;
using NET.Architect.Common;
using NET.Architect.Model;
using NET.BusinessRule;
using NET.Web.UI.Models;
using Newtonsoft.Json;
using PhantomJS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UI;
using UI.Models;

namespace NET.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        TruingLogBLL truinglogbll = new TruingLogBLL();
        public ActionResult Index()
        {
            StatTool.countTool = StatTool.countTool + 1;
            ViewBag.userinfomodel = System.Guid.NewGuid().ToString("N");
            return View();
        }

        public ActionResult CharSession(string message, string key, string s_ip)
        {
            ActionResult result = null;
            string session = string.Empty;
            try
            {
                if (message.Is())
                {
                    #region 字符串去空 截取前面30位
                    message = message.Trim();
                    if (message.Length > 30)
                    {
                        message = message.Substring(0, 30);
                    }
                    #endregion
                    #region 指定问题返回指定答案
                    if (ConstTool.MessageFilter.Contains(message))
                    {
                        session = ConstTool.HomeMessage;
                    }
                    else
                    {
                        UserInfoUnDeteil userinfomodel = new UserInfoUnDeteil();
                        if (key.Is())
                        {
                            userinfomodel = CacheHelper.GetCache(key) as UserInfoUnDeteil;
                            if (userinfomodel.Is())
                            {
                                session = new RequestTuring().GetText(message, userinfomodel);
                            }
                        }
                    }
                    #endregion
                    #region 请求回来的数据二次处理
                    if (session.Is())
                    {
                        foreach (var item in ConstTool.FilterDict)
                        {
                            if (session.Contains(item.Key))
                            {
                                session = session.Replace(item.Key, item.Value);
                            }
                        }
                    }
                    else
                    {
                        session = message;
                    }
                    #endregion
                }
                else
                {
                    session = "未收到您说的什么……";
                }
            }
            finally
            {
                if (!session.Is())
                {
                    session = message;
                }
                result = this.Json(new { session = session }, JsonRequestBehavior.AllowGet);
                FileOperations.WriteLog(key, $"{message}", session, s_ip);//记录成功日志
            }
            return result;
        }



        CustomerUserInfoBLL customeruserinfobll = new CustomerUserInfoBLL();
        UserBLL userBll = new UserBLL();
        KnowledgeBaseBLL bll = new KnowledgeBaseBLL();
        /// <summary>
        /// 机器人聊天界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Chat()
        {
            StatTool.countTool = StatTool.countTool + 1;
            ViewBag.userinfomodel = System.Guid.NewGuid().ToString("N");
            ViewBag.FilterKey = System.Web.HttpUtility.UrlEncode(StatTool.FilterKey);
            ViewBag.countTool = StatTool.countTool;
            return View();
        }
        public JsonResult ChatVerification(RequestParam param)
        {
            #region ThreadPool
            //            List<WaitHandle> waitHandles = new List<WaitHandle>();
            //            ManualResetEvent reset = new ManualResetEvent(false);
            //            waitHandles.Add(reset);
            //            param.WaitHandle = reset;
            //            ThreadPool.QueueUserWorkItem(new WaitCallback(RequestServicesBase.RequestServices_ThreadPool), param);
            //#if DEBUG
            //            WaitHandle.WaitAll(waitHandles.ToArray());
            //#else
            //                WaitHandle.WaitAll(waitHandles.ToArray(), 7000, false);
            //#endif 
            #endregion

            #region Task
            try
            {
                Task.Factory.StartNew(() => { RequestServicesBase.RequestServices_Task(param); });
            }
            catch (Exception ex)
            {
                LoggerHelper.Info("机器人进入验证出错：" + ex.ToString());
            }
            #endregion

            return this.Json(1, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 当前用户登录验证
        /// </summary>
        [ValidateInput(false)]
        public ActionResult UserlegitimacyVerification(string OS, string Ip, string modelType, string Browser_1)
        {
            #region 变量申明
            List<CustomerUserInfo> da1 = new List<CustomerUserInfo>();
            List<CustomerUserInfo> da2 = new List<CustomerUserInfo>();
            List<CustomerUserInfo> da3 = new List<CustomerUserInfo>();
            CustomerUserInfo entity = new CustomerUserInfo();
            int result = 0;
            bool Isflage = true;
            #endregion
            try
            {
                BaiduHelghtIPEntity address = JsonConvert.DeserializeObject<BaiduHelghtIPEntity>(BaiduHelghtIPReqquest.BaidHelghtIp(Ip, modelType));
                CustomerUserInfo data = new CustomerUserInfo();
                //data.OS = GetOSNameByUserAgent(OS);
                data.OS_1 = OS;
                data.Ip = Ip;
                data.Browser_1 = Browser_1;
                try
                {
                    data.modelType = modelType == "mb" ? "手机" : "电脑";
                    data.Isp = address.content.formatted_address;
                    data.lat = address.content.location.Lat;
                    data.lng = address.content.location.Lng;
                    data.Id = address.content.locid;
                    data.Remarks = address.ToConvertJson();
                }
                catch
                {
                }
                if (data != null && Isflage)
                {
                    da1 = customeruserinfobll.Find(data.Ip);
                    if (da1 != null)
                    {
                        entity = da1.Where(n => !string.IsNullOrEmpty(n.Isp)
                                          && !string.IsNullOrEmpty(n.lat)
                                          && !string.IsNullOrEmpty(n.lng)
                                          && !string.IsNullOrEmpty(n.Isp)).First();
                        if (entity != null)
                        {

                        }
                    }
                    #region 同IP OS Browser ISp
                    try
                    {
                        da3 = da1.Where(n => n.Browser.Contains(data.Browser) && n.modelType.Contains(data.modelType) && n.OS.Contains(data.OS) && n.Isp.Contains(data.Isp)).ToList();
                        if (da3.Count > 0 && da3.Count == 1 && Isflage)
                        {
                            entity = da3[0];
                            entity.InCount = entity.InCount + 1;
                            customeruserinfobll.Update(entity);
                            Isflage = false;
                        }
                        else if (da3.Count > 1 && Isflage)
                        {
                            entity = da3[0];
                            entity.InCount = entity.InCount + 1;
                            entity.Remarks = entity.Remarks + da3[0].Id + "此条数据同IP OS Browser有" + da3.Count + "条相同数据   ";
                            #region customeruserinfobll.Update(entity)
                            if (da3[0].modelType != data.modelType)
                            {
                                entity.modelType = da3[0].modelType;
                            }
                            if (da3[0].Browser_1 != data.Browser_1)
                            {
                                entity.Browser_1 = da3[0].Browser_1 + "#" + data.Browser_1;
                            }
                            if (da3[0].Isp != data.Isp)
                            {
                                entity.Isp = da3[0].Isp + "#" + data.Isp;
                            }
                            if (da3[0].OS_1 != data.OS_1)
                            {
                                entity.OS_1 = da3[0].OS_1 + "#" + data.OS_1;
                            }
                            #endregion
                            customeruserinfobll.Update(entity);
                            Isflage = false;
                        }
                    }
                    catch { }
                    #endregion
                    #region 同IP OS ISp
                    try
                    {
                        da2 = da1.Where(n => n.OS.Contains(data.OS) && n.Isp.Contains(data.Isp)).ToList();
                        if (da2.Count > 0 && da2.Count == 1 && Isflage)
                        {
                            entity = da2[0];
                            entity.InCount = entity.InCount + 1;
                            customeruserinfobll.Update(entity);
                            Isflage = false;
                        }
                        else if (da2.Count > 1 && Isflage)
                        {
                            entity = da2[0];
                            entity.InCount = entity.InCount + 1;
                            entity.Remarks = entity.Remarks + da2[0].Id + "此条数据同IP OS ISp 有" + da2.Count + "条相同数据   ";
                            #region customeruserinfobll.Update(entity)
                            if (da2[0].Browser_1 != data.Browser_1)
                            {
                                entity.Browser_1 = da2[0].Browser_1 + "#" + data.Browser_1;
                            }
                            if (da2[0].Isp != data.Isp)
                            {
                                entity.Isp = da2[0].Isp + "#" + data.Isp;
                            }
                            if (da2[0].OS_1 != data.OS_1)
                            {
                                entity.OS_1 = da2[0].OS_1 + "#" + data.OS_1;
                            }
                            #endregion
                            customeruserinfobll.Update(entity);
                            Isflage = false;
                        }
                    }
                    catch { }
                    #endregion
                    #region 同IP有多个不同系统和游览器
                    try
                    {
                        if (da1.Count > 0 && da1.Count == 1 && Isflage)
                        {
                            entity = da1[0];
                            entity.InCount = entity.InCount + 1;
                            customeruserinfobll.Update(entity);
                            Isflage = false;
                        }
                        else if (da1.Count() > 1 && Isflage)
                        {
                            entity = da1[0];
                            entity.InCount = entity.InCount + 1;
                            entity.Remarks = entity.Remarks + da1[0].Id + "此条数据同IP有" + da1.Count + "条相同数据但是不同IP OS Browser  ";
                            #region customeruserinfobll.Update(entity)
                            if (da1[0].Browser != data.Browser)
                            {
                                entity.Browser_1 = da2[0].Browser_1 + "#" + data.Browser_1;
                            }
                            if (da1[0].Isp != data.Isp)
                            {
                                entity.Isp = da1[0].Isp + "#" + data.Isp;
                            }
                            if (da1[0].OS_1 != data.OS_1)
                            {
                                entity.OS_1 = da1[0].OS_1 + "#" + data.OS_1;
                            }
                            #endregion
                            customeruserinfobll.Update(entity);
                            Isflage = false;
                        }
                    }
                    catch { }

                    #endregion
                    #region 没有新增
                    if (Isflage)
                    {
                        Random rd = new Random();
                        data.Status = 1;
                        data.InCount = 1;
                        data.IMG = rd.Next(0, 12);
                        result = 1;
                        Isflage = false;
                        customeruserinfobll.Add(data);
                        CacheHelper.SetCache("CustomerUserInfo", data);
                    }
                    else
                    {
                        result = entity.Status;
                        CacheHelper.SetCache("CustomerUserInfo", entity);
                    }
                    LoggerHelper.Info("用户进入信息记录：" + data.ToConvertJson());
                    #endregion
                    StatTool.countTool++;
                    //customeruserinfobll.Add(data);
                }
            }
            catch (Exception ec) { string s = ec.ToString(); }
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 知识库详细列表
        /// </summary>
        /// <returns></returns>
        public ActionResult NewKnowList()
        {
            var userModel = Session["CurrentUserInfo"] as User;
            if (userModel == null)
                return RedirectToAction("Login");
            return View(bll.Find().OrderByDescending(n => n.LAST_MODIFIED_TIME).Take(200));
        }
        #region 知识库编辑
        public ActionResult Edit(string id)
        {
            var userModel = Session["CurrentUserInfo"] as User;
            if (userModel == null)
                return RedirectToAction("Login");
            var modelList = bll.Find(id)[0];
            return View(modelList);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateData(string info, string text)
        {
            int status = 0;
            string result = string.Empty;
            try
            {
                var userModel = Session["CurrentUserInfo"] as User;
                if (userModel == null)
                    return RedirectToAction("Login");
                var modelList = bll.Find(info)[0];
                if (modelList != null)
                {
                    modelList.Text = text;
                    modelList.LAST_MODIFIED_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    List<KnowledgeBase> list = new List<KnowledgeBase>();
                    list.Add(modelList);
                    bll.Update(list);
                    status = 1;
                    return RedirectToAction("NewKnowList");
                }
            }
            catch
            {
            }
            result = "保存失败";
            return this.Json(new { status, result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 知识库删除
        [HttpPost]
        public ActionResult DeleteData(string info)
        {
            int status = 0;
            string result = string.Empty;
            try
            {
                var userModel = Session["CurrentUserInfo"] as User;
                if (userModel == null)
                    return RedirectToAction("Login");
                var modelList = bll.Find(info)[0];
                if (modelList != null)
                {
                    List<KnowledgeBase> list = new List<KnowledgeBase>();
                    list.Add(modelList);
                    int count = bll.Delete(list);
                    if (count > 0)
                    {
                        status = 1;
                        result = "成功删除" + count + "条数据";
                        return RedirectToAction("NewKnowList");
                    }
                    else
                    {
                        status = 0;
                        result = "保存失败";
                    }
                }
            }
            catch
            {
                status = 0;
                result = "保存失败";
            }
            return this.Json(new { status, result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(string id)
        {
            try
            {
                var userModel = Session["CurrentUserInfo"] as User;
                if (userModel == null)
                    return RedirectToAction("Login");
                var modelList = bll.Find(id)[0];
                return View(modelList);
            }
            catch
            {
                return RedirectToAction("NewKnowList");
            }
        }
        #endregion
        #region 知识库新建
        public ActionResult NewKnow()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult PostNewKnow(KnowledgeBase pivoData)
        {
            int status = 0;
            var result = string.Empty;
            try
            {
                if (pivoData != null && pivoData.Info != null && pivoData.Text != null)
                {
                    if (StatTool.KeyFilter(pivoData.Text) && StatTool.KeyFilter(pivoData.Info))
                    {
                        string AddContext = StatTool.ConvertFileStr(pivoData.Text.Trim());
                        if (pivoData.Code == "555003")
                            AddContext = string.Format("<a href='{0}' class='btn btn-danger' title='{0}'>{0}</a>", AddContext);
                        KnowledgeBase model = new KnowledgeBase();
                        model.Code = StatTool.ConvertFileStr(pivoData.Code.Trim());
                        model.Info = StatTool.ConvertFileStr(pivoData.Info);
                        model.Text = AddContext;
                        model.UserID = pivoData.UserID;
                        model.LAST_MODIFIED_TIME = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        model.Ip = pivoData.Ip;
                        model.ClientAddressJson = pivoData.ClientAddressJson;
                        KnowledgeBaseBLL bll = new KnowledgeBaseBLL();
                        result = string.Format("成功添加知识库{0}条，可以到会话窗口进行查看，感谢支持", bll.Add(model));
                        status = 1;
                        // new CustomizeTuringLoginc().CaseRefresh();
                    }
                    else
                        result = "你键入的字符有违反国家法律法规不允许的字符，请确认";
                }
                else
                    result = "必填字段不可为空，请确认";
            }
            catch (System.Exception ex)
            {
                status = 0;
                result = string.Format("此条数据知识库已经存在请换一条在试试");
            }
            return this.Json(new { result, status }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 知识库登陆
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 用户登录方法
        /// </summary>
        /// <param name="username">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginData(string username, string password)
        {
            int status = 0;
            string result = string.Empty;
            try
            {
                var model = userBll.Find(username)[0];
                if (model.password == password)
                {
                    status = 1;
                    Session["CurrentUserInfo"] = model;
                    // return RedirectToAction("NewKnowList");
                }
                else
                    result = "登录失败，用户或密码错误";
            }
            catch
            {
                result = "登录失败，用户或密码错误";
            }
            return this.Json(new { status, result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 知识库详细页面
        public ActionResult Details(string id)
        {
            try
            {
                var userModel = Session["CurrentUserInfo"] as User;
                if (userModel == null)
                    return RedirectToAction("Login");
                var modelList = bll.Find(id)[0];
                return View(modelList);
            }
            catch
            {
                return RedirectToAction("NewKnowList");
            }
        }
        #endregion


        public byte[] CreateValidateGraphic
        {
            get
            {

                string chkCode = string.Empty;

                //颜色列表，用于验证码、噪线、噪点 

                Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };

                //字体列表，用于验证码 

                string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };

                char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

                Random rnd = new Random();

                //生成验证码字符串 

                for (int i = 0; i < 4; i++)

                {

                    chkCode += character[rnd.Next(character.Length)];

                }

                Bitmap bmp = new Bitmap(100, 40);

                Graphics g = Graphics.FromImage(bmp);

                g.Clear(Color.White);

                //画噪线 

                for (int i = 0; i < 10; i++)

                {

                    int x1 = rnd.Next(100);

                    int y1 = rnd.Next(40);

                    int x2 = rnd.Next(100);

                    int y2 = rnd.Next(40);

                    Color clr = color[rnd.Next(color.Length)];

                    g.DrawLine(new Pen(clr), x1, y1, x2, y2);

                }

                //画验证码字符串 

                for (int i = 0; i < chkCode.Length; i++)

                {

                    string fnt = font[rnd.Next(font.Length)];

                    Font ft = new Font(fnt, 18);

                    Color clr = color[rnd.Next(color.Length)];

                    g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);

                }

                //画噪点 

                for (int i = 0; i < 100; i++)

                {

                    int x = rnd.Next(bmp.Width);

                    int y = rnd.Next(bmp.Height);

                    Color clr = color[rnd.Next(color.Length)];

                    bmp.SetPixel(x, y, clr);

                }

                g.DrawRectangle(new Pen(Color.Silver), 0, 0, bmp.Width - 1, bmp.Height - 1);

                MemoryStream stream = new MemoryStream();

                bmp.Save(stream, ImageFormat.Jpeg);

                Session["code"] = chkCode;

                return stream.ToArray();

            }
        }
        public ActionResult GetValidateCode()
        {
            return File(CreateValidateGraphic, @"image/jpeg");
        }


        public ActionResult ValidateCode(string var)
        {
            int result = 0;
            try
            {
                if (Session["code"].ToString().ToUpper() == var.ToUpper())
                { result = 1; }
            }
            catch
            {
            }
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
        private int FileterCounter = Convert.ToUInt16(ConfigurationManager.AppSettings["FileterCounter"]);
        private int FileterMinutes = Convert.ToInt32(string.Format("-{0}", ConfigurationManager.AppSettings["FileterMinutes"]));

        public ActionResult Phantom()
        {
            return View();
        }
        public ActionResult Crawler(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                var Directory = string.Format("/Content/CrawlerImae/{0}/{1}.Png", DateTime.Now.ToString("yyMM"), DateTime.Now.ToString("ddHHmmss"));
                var path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, Directory);
                var html = CrawlerHelper.PhantomJs(url, ref path);
                return Json(new { href = string.Format("../../{0}", Directory), content = html }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}