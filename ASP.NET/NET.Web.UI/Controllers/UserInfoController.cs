using NET.Architect.Model;
using NET.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NET.Web.UI.Controllers
{
    public class UserInfoController : Controller
    {
        CustomerUserInfoBLL customeruserinfobll = new CustomerUserInfoBLL();
        // GET: UserInfo
        public ActionResult Index()
        {
            var userModel = Session["CurrentUserInfo"] as User;
            if (userModel == null)
                return RedirectToAction("Login", "Home");
            var model = customeruserinfobll.Find().OrderByDescending(n => n.OperationTime).Take(200);
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");
                else
                {
                    var userModel = Session["CurrentUserInfo"] as User;
                    if (userModel == null)
                        return RedirectToAction("Login");
                    var modelList = customeruserinfobll.FindID(id)[0];
                    return View(modelList);
                }
            }
            catch
            {
                return View("Index");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateData(string Status, string IMG, string Id)
        {
            int status = 0;
            string result = string.Empty;
            try
            {
                var userModel = Session["CurrentUserInfo"] as User;
                if (userModel == null)
                    return RedirectToAction("Login");
                var modelList = customeruserinfobll.FindID(Id)[0];
                if (modelList != null)
                {
                    modelList.IMG = Convert.ToInt32(IMG);
                    modelList.Status = Convert.ToInt32(Status);
                    if (modelList.IMG < 0 || modelList.IMG > 12)
                    {
                        status = 0;
                        result = "图片验证失败，图片序列只能在0-12范围内";
                    }
                    else if (modelList.Status < 0 || modelList.Status > 12)
                    {
                        status = 0;
                        result = "状态验证失败，只能大于0范围内，0为黑名单";
                    }
                    else
                    {
                        modelList.OperationTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        List<CustomerUserInfo> list = new List<CustomerUserInfo>();
                        list.Add(modelList);
                        customeruserinfobll.Update(list);
                        status = 1;
                        result = "保存成功";
                    }
                }
            }
            catch
            {
                status = 0;
                result = "发生异常";
            }
            result = "保存失败";
            return this.Json(new { status, result }, JsonRequestBehavior.AllowGet);
        }
    }
}