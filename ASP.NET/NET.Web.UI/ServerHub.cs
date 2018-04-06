using DotNet.Utilities;
using Microsoft.AspNet.SignalR;
using NET.Architect.Common;
using UI;
using UI.Models;
using NET.Architect.Model;
using NET.BusinessRule;
using System.Threading.Tasks;
using System;

namespace NET.Web.UI
{
    public class ServerHub : Hub
    {
        TruingLogBLL truinglogbll = new TruingLogBLL();
        CustomerUserInfoBLL customeruserinfobll = new CustomerUserInfoBLL();

        /// <summary>
        /// 供客户端调用的服务器端代码
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message, string key,string s_ip)
        {
            Task.Factory.StartNew(() => { CharSession(message, key, s_ip); });
        }
        private void CharSession(string message, string key, string s_ip)
        {
            string Flage = string.Empty;
            string CutomerUser = string.Empty;
            string session = string.Empty;
            try
            {
                if (message.Is() && key.Is())
                {
                    #region 字符串去空 截取前面30位
                    message = message.Trim();
                    if (message.Length > 30)
                        message = message.Substring(0, 30);
                    #endregion
                    #region 会画处理
                    UserInfoUnDeteil userinfomodel = CacheHelper.GetCache(key) as UserInfoUnDeteil;
                    if (!userinfomodel.Is() && !userinfomodel.UserInfo.Status.Is())
                    {
                        userinfomodel.UserInfo = new UserInfoBLL().Find(new UserInfo() { Id = key })[0];
                        userinfomodel.UserInfoDetail = new UserInfoDetailBLL().Find(new UserInfoDetail() { Ip = userinfomodel.UserInfo.Ip })[0];
                    }
                    if (!userinfomodel.Is() && !userinfomodel.UserInfo.Status.Is())
                    {
                        userinfomodel.UserInfo = new UserInfoBLL().Find(new UserInfo() { Ip = s_ip })[0];
                        userinfomodel.UserInfoDetail = new UserInfoDetailBLL().Find(new UserInfoDetail() { Ip = userinfomodel.UserInfo.Ip })[0];
                    }
                    if (userinfomodel.Is() && userinfomodel.UserInfo.Status.Is() && userinfomodel.UserInfo.Is() && userinfomodel.UserInfoDetail.Is())
                    {
                        userinfomodel.Message = message;
                        userinfomodel.Key = key;
                        #region 组装成员
                        CutomerUser = string.Format(ConstTool.MessageFormat, userinfomodel.Message.ToSBC(), userinfomodel.UserInfo.Isp, userinfomodel.UserInfo.Img, userinfomodel.UserInfoDetail.OS, DateTime.Now.ToString(ConstTool.DateFormat2), userinfomodel.UserInfoDetail.ModelType, userinfomodel.UserInfo.Ip);
                        #region 指定问题返回指定答案
                        if (ConstTool.MessageFilter.Contains(userinfomodel.Message))
                            session = ConstTool.HomeMessage;
                        else
                            session = new RequestTuring().GetText(userinfomodel.Message, userinfomodel);
                        #endregion
                        #region 请求回来的数据二次处理
                        foreach (var item in ConstTool.FilterDict)
                        {
                            if (session.Contains(item.Key))
                                session = session.Replace(item.Key, item.Value);
                        }
                        #endregion
                       // Flage = RequestVerifi(userinfomodel);
                        #endregion
                    }
                    else
                    {
                        var user = new UserInfoBLL().Find(new UserInfo() { Id = key });
                        Flage = "系统识别您是非法用户，清稍后在试试……";
                        Task.Factory.StartNew(() => { truinglogbll.Add(new TruingLog() { ID = key, UserId = userinfomodel.UserInfo.Ip, SESSION = $"{session}-{Flage}", INFO = message, LAST_MODIFIED_TIME = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }); });
                    }
                    #endregion
                }
                else
                {
                    Flage = "非法用户，清稍后在试试……";
                    Task.Factory.StartNew(() => { truinglogbll.Add(new TruingLog() { ID = key, UserId = System.Web.HttpContext.Current.Request.Browser.Browser, SESSION = $"{session}-{Flage}", INFO = message, LAST_MODIFIED_TIME = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }); });
                }
            }
            finally
            {
                Clients.All.sendMessage(session, StatTool.countTool, CutomerUser, key, Flage);
                FileOperations.WriteLog(key,$"{message}-{ Flage}", session, s_ip);//记录成功日志
            }
        }
        public string RequestVerifi(UserInfoUnDeteil userinfomodel)
        {
            #region 恶意检查
            string sql = "select Count(1) from TruingLog a where LAST_MODIFIED_TIME>'{0}' and LAST_MODIFIED_TIME<'{1}' and UserId='{2}' and SESSION='{3}'";
            sql = string.Format(sql, System.DateTime.Now.AddMinutes(-2).ToString(ConstTool.DateFormat1), System.DateTime.Now.ToString(ConstTool.DateFormat1), userinfomodel.UserInfo.Ip, userinfomodel.Message);
            if (truinglogbll.Excute(sql) > 20000)
            {
                CustomerUserInfo entity = new CustomerUserInfo();
                entity.Status = 0;
                entity.Id = userinfomodel.UserInfo.Ip;
                customeruserinfobll.Update(entity);
                CacheHelper.RemoveAllCache("CustomerUserInfo");
                userinfomodel.UserInfo.Status = 0;
                new UserInfoBLL().Update(userinfomodel.UserInfo);
                CacheHelper.RemoveAllCache(userinfomodel.Key);
                CacheHelper.SetCache(userinfomodel.Key, userinfomodel);
                return "您2分钟内超20000次操作涉嫌多次恶意操作制进入黑名单";
            }
            return string.Empty;
            #endregion
        }
    }
}