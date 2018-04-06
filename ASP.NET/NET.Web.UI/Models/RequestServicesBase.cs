using APIRequestServices;
using DotNet.Utilities;
using Log4net;
using NET.Architect;
using NET.Architect.Common;
using NET.Architect.Model;
using NET.BusinessRule;
using NET.DataAccessLayer;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UI;

namespace NET.Web.UI.Models
{
    public class RequestServicesBase
    {
        #region 1 数据请求
        private static BaiduHelghtIPEntity GetBaiduHelghtIpRequestServices(RequestParam param)
        {
            return HelpComm.JsonToObject<BaiduHelghtIPEntity>(BaiduHelghtIPReqquest.BaidHelghtIp(param.Ip, param.ModelType));
        }
        private static Root GetRootRequestServices(RequestParam param)
        {
            return HelpComm.JsonToObject<Root>(BaidOrdinaryRequest.BaidApiGetRequestIpAddress(param.Ip));
        }
        private static RegionalCascadeAddres GetRegionalCascadeAddresRequestServices(RequestParam param)
        {
            return HelpComm.JsonToObject<RegionalCascadeAddres>(param.Data);
        }
        #endregion

        #region 2 数据组装
        private static void RequestServeicesResponse(RequestParam param,ref UserInfoUnDeteil userinfoundeteil)
        {
            #region 变量申明
            bool Flage = true;
            UserInfoBLL userInfoBll = new UserInfoBLL();
            UserInfo userInfo = new UserInfo();
            UserInfoDetail userInfoDetail = new UserInfoDetail();
            UserInfoUnDeteil userinfomodel = new UserInfoUnDeteil();
            string modelType;
            #endregion

            #region 组装UserInfoDetail
            userInfoDetail.Remarks = param.UserAgent != null ? param.UserAgent : System.Web.HttpContext.Current.Request.UserAgent;
            userInfoDetail.Id = System.Guid.NewGuid().ToString("N");
            userInfoDetail.Ip = param.Ip;
            userInfoDetail.OS = HelpComm.GetOSNameByUserAgent(userInfoDetail.Remarks, out modelType);
            userInfoDetail.ModelType = modelType;
            userInfoDetail.OperationTime = System.DateTime.Now.ToString(ConstTool.DateFormat1);
            userInfoDetail.Browser = param.UserAgent != null ? param.UserAgent : $"{System.Web.HttpContext.Current.Request.Browser.Browser} { System.Web.HttpContext.Current.Request.Browser.Version}";
            #endregion

            #region  新用户
            userInfo.Ip = userInfoDetail.Ip;
            var user = userInfoBll.Find(userInfo);
            if (user == null || user.Count <= 0)
            {
                #region 组装UserInfo
                userInfo.Img = new Random().Next(0, 12);
                userInfo.Id = param.Token;
                userInfo.OperationTime = System.DateTime.Now.ToString(ConstTool.DateFormat1);
                userInfo.InCount = 1;
                userInfo.Status = 1;
                #region 定位
                BaiduHelghtIPEntity address = GetBaiduHelghtIpRequestServices(new RequestParam() { Ip = userInfo.Ip, ModelType = userInfoDetail.ModelType });
                LoggerHelper.Info("百度API请求  ：" +
                    Environment.NewLine + "请求参数是：" + $"Ip={userInfo.Ip} Type={userInfoDetail.ModelType}" +
                    Environment.NewLine + address.ToConvertJson());
                if (address.result.error != "161")
                {
                    #region 二次定位请求
                    var requestData = GetRootRequestServices(new RequestParam() { Ip = userInfo.Ip });
                    if (!requestData.error.Is() && requestData.data.Is() && requestData.Is() && !requestData.data.Province.Is() && requestData.data.City.Is())
                    {
                        #region 普通定位请求
                        userInfo.Isp = $"{requestData.data.Country}{ requestData.data.Province}{ requestData.data.City}";
                        userInfo.Lat = requestData.data.Lat;
                        userInfo.Lng = requestData.data.Lng;
                        userInfo.City = requestData.data.City;
                        userInfo.Province = requestData.data.Province;
                        userInfo.Country = requestData.data.Country;
                        userInfo.Remarks = "普通定位请求：" + requestData.ToConvertJson();
                        #endregion
                    }
                    else
                    {
                        #region 普通定位失败JS请求
                        if (param.Data != null)
                        {
                            RegionalCascadeAddres regionalAddress = GetRegionalCascadeAddresRequestServices(param);
                            if (regionalAddress != null)
                            {
                                userInfo.Isp = $"{regionalAddress.Country}{ regionalAddress.Province}{ regionalAddress.City}";
                                userInfo.City = regionalAddress.City;
                                userInfo.Province = regionalAddress.Province;
                                userInfo.Country = regionalAddress.Country;
                                userInfo.Remarks = "普通定位失败JS请求：" + regionalAddress.ToConvertJson();
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(userInfo.Ip))
                                {
                                    userInfo.Ip = "未知";
                                    userInfo.Isp = "未知";
                                    userInfo.City = "未知";
                                    userInfo.Province = "未知";
                                    userInfo.Country = "未知";
                                    userInfo.Remarks = "未知";
                                }
                                else
                                {
                                    userInfo.Isp = userInfo.Ip;
                                    userInfo.City = userInfo.Ip;
                                    userInfo.Province = userInfo.Ip;
                                    userInfo.Country = userInfo.Ip;
                                    userInfo.Remarks = userInfo.Ip;
                                }

                                LoggerHelper.Info(
                                   Environment.NewLine + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name +
                                   Environment.NewLine + "三次Ip定位都全部失败" +
                                   Environment.NewLine + "发生时间" + DateTime.Now.ToString(ConstTool.DateFormat1) +
                                   Environment.NewLine + "Ip：" + param.Ip +
                                   Environment.NewLine + "Token：" + param.Token +
                                   Environment.NewLine + "c#后台IP定位是：" + StatTool.GetNetIp +
                                   Environment.NewLine + "data：" + param.Data
                                   );
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 百度Ip高精度定位成功组装成对象
                    userInfo.Isp = address.content.formatted_address;
                    userInfo.Lat = address.content.location.Lat;
                    userInfo.Lng = address.content.location.Lng;
                    userInfo.Locid = address.content.locid;
                    userInfo.Province = address.content.address_component.Province;
                    userInfo.City = address.content.address_component.City;
                    userInfo.Country = address.content.address_component.Country;
                    userInfo.District = address.content.address_component.District;
                    userInfo.Street = address.content.address_component.Street;
                    userInfo.Street_Number = address.content.address_component.Street_Number;
                    userInfo.Admin_Area_Code = address.content.address_component.admin_area_code;
                    userInfo.Confidence = address.content.confidence;
                    userInfo.radius = address.content.radius;
                    userInfo.Remarks = address.ToConvertJson();
                    #endregion
                }
                #endregion
                #endregion
                userInfo.OperationTime = DateTime.Now.ToString(ConstTool.DateFormat1);
                userInfoBll.Add(userInfo);
            }
            #endregion

            #region 老用户更新访问次数
            else
            {
                var ent = user.Where(n => n.Status == 1).ToList();
                if (ent != null && ent.Count > 0)
                {
                    userInfo = ent.FirstOrDefault();
                    userInfo.InCount = userInfo.InCount + 1;
                    userInfoBll.Update(userInfo);
                }
                else
                    Flage = false;//说明此用户进入黑名单了
            }
            #endregion

            userinfoundeteil= new UserInfoUnDeteil { UserInfo = userInfo, UserInfoDetail = userInfoDetail, Flage = Flage };
        }
        #endregion
        #region 3 信息记录
        private static void RequestServicesRecording(RequestParam param)
        {
            UserInfoUnDeteil response = new UserInfoUnDeteil();
            var watch = HelpComm.Stopwatch(() => RequestServeicesResponse(param, ref response));
            #region 合法用户记录用户信息 写入明细表
            if (response.Flage)
            {
                response.UserInfoDetail.Remarks = "本次共用时：" + watch.ElapsedMilliseconds.ToString() + Environment.NewLine + response.UserInfoDetail.Remarks;
                response.Key = param.Token;
                new UserInfoDetailDAL().Add(response.UserInfoDetail);
                CacheHelper.SetCache(response.Key, response);
            }
            #endregion
            #region 记录日志
            else
                LoggerHelper.Info(
                          Environment.NewLine + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name +
                          Environment.NewLine + "验证失败" +
                          Environment.NewLine + "发生时间" + DateTime.Now.ToString(ConstTool.DateFormat1) +
                          Environment.NewLine + "本次共用时：" + watch.ElapsedMilliseconds.ToString() +
                          Environment.NewLine + "Ip：" + param.Ip +
                          Environment.NewLine + "Token：" + param.Token +
                          Environment.NewLine + "c#后台IP定位是：" + StatTool.GetNetIp +
                          Environment.NewLine + "userinfomodel：" + response.ToConvertJson()
                          );
            #endregion
        }
        #endregion

        #region 4 ThreadPool线程调用方法
        public static void RequestServices_ThreadPool(object obj)
        {
            Stopwatch watch = new Stopwatch();
            RequestParam param = obj as RequestParam;
            ManualResetEvent are = (ManualResetEvent)param.WaitHandle;
            try
            {
                watch = HelpComm.Stopwatch(RequestServicesRecording, param);
            }
            catch (Exception e)
            {
                #region 记录异常
                LoggerHelper.Error(
                            Environment.NewLine + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name +
                            Environment.NewLine + "发生异常" +
                            Environment.NewLine + "发生时间" + DateTime.Now.ToString(ConstTool.DateFormat1) +
                            Environment.NewLine + "本次共用时：" + watch.ElapsedMilliseconds.ToString() +
                            Environment.NewLine + "异常讯息：" + e.ToString() +
                            Environment.NewLine + "请求参数是 ：" + obj.ToConvertJson());
                #endregion
            }
            finally
            {
                are.Set();
            }
        }
        #endregion

        #region 5 Task线程调用方法
        public static void RequestServices_Task(RequestParam param)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                watch = HelpComm.Stopwatch(RequestServicesRecording, param);
            }
            catch (Exception e)
            {
                #region 记录异常
                LoggerHelper.Error(
                            Environment.NewLine + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name +
                            Environment.NewLine + "发生异常" +
                            Environment.NewLine + "发生时间" + DateTime.Now.ToString(ConstTool.DateFormat1) +
                            Environment.NewLine + "本次共用时：" + watch.ElapsedMilliseconds.ToString() +
                            Environment.NewLine + "异常讯息：" + e.ToString() +
                            Environment.NewLine + "请求参数是 ：" + param.ToConvertJson());
                #endregion
            }
        }
        #endregion
    }
}