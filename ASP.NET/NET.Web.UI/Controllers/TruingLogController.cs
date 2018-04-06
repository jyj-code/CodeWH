using NET.Architect.Model;
using NET.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NET.Web.UI.Controllers
{
    public class TruingLogController : Controller
    {
        TruingLogBLL logBll = new TruingLogBLL();
        // GET: TruingLog
        public ActionResult List()
        {
            var userModel = Session["CurrentUserInfo"] as User;
            if (userModel == null)
                return RedirectToAction("Login", "Home");
            var model = logBll.Find().OrderByDescending(n => n.LAST_MODIFIED_TIME).Take(200);
            return View(model);
        }
        public ActionResult Index()
        {
            var userModel = Session["CurrentUserInfo"] as User;
            if (userModel == null)
                return RedirectToAction("Login", "Home");
            var model = logBll.Find().OrderByDescending(n => n.LAST_MODIFIED_TIME).Take(200);
            return View(model);
        }
    }
}