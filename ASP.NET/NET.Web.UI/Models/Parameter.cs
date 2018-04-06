using APIRequestServices;
using DotNet.Utilities;
using NET.Architect.Model;
using NET.BusinessRule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UI.Models.Turing
{
    /// <summary>
    ///  调用图灵API参数
    /// </summary>
    public class Parameter
    {
        public Parameter(string info, UserInfoUnDeteil userinfo)
        {
            this.info = info;
            if (userinfo != null && userinfo.UserInfo.Lng == null && userinfo.UserInfo.Lat == null)
            {
                try
                {
                    var ClientIpAddress = JsonConvert.DeserializeObject<Root>(BaidOrdinaryRequest.BaidApiGetRequestIpAddress(userinfo.UserInfo.Ip));
                    if (ClientIpAddress.error == 0 && ClientIpAddress.data != null)
                    {
                        userinfo.UserInfo.Lat = ClientIpAddress.data.Lat;
                        userinfo.UserInfo.Lng = ClientIpAddress.data.Lng;
                        if (!userinfo.UserInfo.Isp.Contains(ClientIpAddress.data.Province) || !userinfo.UserInfo.Isp.Contains(ClientIpAddress.data.City))
                        {
                            userinfo.UserInfo.Isp = userinfo.UserInfo.Isp + "[" + ClientIpAddress.data.Province + ClientIpAddress.data.City + ClientIpAddress.data.Country + "]";
                        }
                        UserInfoBLL userinfobll = new UserInfoBLL();
                        userinfobll.Update(userinfo.UserInfo);
                        CacheHelper.RemoveAllCache(userinfo.Key);
                        CacheHelper.SetCache(userinfo.Key, userinfo);
                    }
                }
                catch { }
                int result;
                int.TryParse(userinfo.UserInfo.Ip.Replace(".", ""), out result);
                if (result != 0)
                    userid = result;
                else
                    userid = 123;
                double reslut_lng;
                Double.TryParse(userinfo.UserInfo.Lng, out reslut_lng);
                if (reslut_lng > 1)
                {
                    this.lon = Convert.ToDouble(reslut_lng).ToString("0.000000");
                }
                double reslut_lat;
                Double.TryParse(userinfo.UserInfo.Lat, out reslut_lat);
                if (reslut_lat > 1)
                {
                    this.lat = Convert.ToDouble(reslut_lat).ToString("0.000000");
                }
                if (userinfo.UserInfo.Isp.Contains(" "))
                {
                    this.loc = userinfo.UserInfo.Isp.Split(' ')[0];
                }
                else
                    this.loc = userinfo.UserInfo.Isp;
                return;
            }
        }
        public string key { get { return ConstParameter.Turing_key; } }
        public string info { get; set; }
        public string loc { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
        /// <summary>
        /// 上写文ID
        /// </summary>
        public int userid { get; set; }
    }
}
#region 文字类  和其他类别基础类

public class TuringCustom : turingText
{
    public List<turingText> CShare { get; set; }
}

/// <summary>
/// 文字类
/// </summary>
public class turingText
{
    /// <summary>
    /// 标示 识别码
    /// </summary>
    public string code { get; set; }
    /// <summary>
    /// 提示语
    /// </summary>
    public string text { get; set; }
}
#endregion
#region  链接类  和  列车类 ，航班类
/// <summary>
/// 链接类  和  列车类 ，航班类
/// </summary>
public class Links : turingText
{
    public string url { get; set; }
}
#endregion
#region 新闻类
/// <summary>
/// 新闻类
/// </summary>
public class News : turingText
{
    /// <summary>
    /// 新闻列表
    /// </summary>
    public List<articleList> list { get; set; }

}
/// <summary>
/// 文章列表
/// </summary>
public class articleList
{
    /// <summary>
    /// 新闻标题
    /// </summary>
    public string article { get; set; }
    /// <summary>
    /// 新闻来源
    /// </summary>
    public string source { get; set; }
    /// <summary>
    /// 新闻图片
    /// </summary>
    public string icon { get; set; }
    /// <summary>
    /// 新闻链接地址
    /// </summary>
    public string detailurl { get; set; }
}
#endregion
#region 菜谱类
/// <summary>
/// 菜谱类
/// </summary>
public class menuClass : turingText
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public List<menuList> list { get; set; }
}
public class menuList
{
    /// <summary>
    /// 菜名
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 菜谱信息
    /// </summary>
    public string info { get; set; }
    /// <summary>
    /// 详情链接
    /// </summary>
    public string detailurl { get; set; }
    /// <summary>
    /// 信息图标
    /// </summary>
    public string icon { get; set; }
}
#endregion
#region 儿歌列表
/// <summary>
/// 儿歌类
/// </summary>
public class ChildrenIsSongs : turingText
{
    /// <summary>
    /// 儿歌列表
    /// </summary>
    public List<ChildrenIsSongsList> function { get; set; }
}
public class ChildrenIsSongsList
{
    /// <summary>
    /// 歌曲名
    /// </summary>
    public string song { get; set; }
    /// <summary>
    /// 歌手
    /// </summary>
    public string singer { get; set; }
}
#endregion
#region 诗词类
/// <summary>
/// 诗词类
/// </summary>
public class Poetry : turingText
{
    /// <summary>
    /// 诗词列表
    /// </summary>
    public List<PoetryList> function { get; set; }
}
public class PoetryList
{
    /// <summary>
    /// 作者
    /// </summary>
    public string author { get; set; }
    /// <summary>
    /// 诗词名
    /// </summary>
    public string name { get; set; }
    #endregion
}