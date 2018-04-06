using KebueUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Common;
using BaiduSDK.Entity;

namespace KebueUI.Controllers
{
    public class ChatController : Controller
    {
        static int c = 0;
        // GET: Chat
        public ActionResult Index()
        {
            c += 1;
            ViewBag.countTool = c;
            return View();
        }
        public JsonResult ChatVerification(string UserAgent, string cip)
        {
            var IPArr = cip.GetHashCode();
            var chat = new ChatRequest()
            {
                UserAgent = UserAgent,
                Ip = cip,
                Os = CommonHelp.GetOSNameByUserAgent(UserAgent),
                ModelType = CommonHelp.modelType,
            };
            if (Request.Cookies.ContainsKey(constStr.Uid))
                chat.mapEntity = Request.Cookies[constStr.Uid].ToObject<ChatRequest>().mapEntity;
            else
                chat.mapEntity = HttpRequestHelp.BaidHelghtIp(cip, CommonHelp.modelType);
            Response.Cookies.Append(constStr.Uid, Newtonsoft.Json.JsonConvert.SerializeObject(chat));
            return this.Json(chat);
        }
        public JsonResult ChartSend(string info)
        {
            info = HttpRequestHelp.RequestTuringServices(info, Request.Cookies[constStr.Uid].ToObject<ChatRequest>());
            return this.Json(info);
        }
        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}