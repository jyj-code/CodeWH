using BaiduSDK.Entity;
using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace KebueUI.Models
{
    public class HttpRequestHelp
    {
        /// <summary>
        /// 根据IP获取地理位置信息
        /// </summary>
        /// <param name="LocationIP"></param>
        /// <param name="ModelType"></param>
        /// <returns></returns>
        public static MapEntity BaidHelghtIp(string LocationIP, string ModelType)
        {
            string strURL = $"https://api.map.baidu.com/highacciploc/v1?qcip={LocationIP}&qterm={ModelType}&ak=bDSpgtzrQvEEhAo1NdEEHjWo7f2KTiTn&extensions=3";
            Dictionary<object, object> requestpare = new Dictionary<object, object>();
            requestpare.Add("qcip", LocationIP);
            requestpare.Add("qterm", ModelType);
            return HttpHelper.HttpWebRequests(strURL, requestpare).ToObject<MapEntity>();
        }

        /// 图灵机器人请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string RequestTuringServices(string info, ChatRequest Parameter)
        {
            var response = HttpHelper.RequestTuringServices(info, Parameter);
            return TuringAnswerText(response.ToObject<TuringEntity>().code, response);
        }

        public static string TuringAnswerText(string code, string response)
        {
            int i = 0;
            StringBuilder strub;
            string temp = string.Empty;
            string returnStr = string.Empty;
            switch (code)
            {
                //链接 列出 航班类
                case "200000":
                    Links links = response.ToObject<Links>();
                    returnStr = string.Format("{0}<br /><a href='{1}' title='链接 列出 航班类  {2}' target='_blank'>{1}</a>", links.text, links.url, links.code);
                    break;
                //新闻类
                case "302000":
                    News news = response.ToObject<News>();

                    strub = new StringBuilder();
                    i = 0;
                    temp = " <li>{0}<ul><li>新闻标题：{1}</li><li>新闻来源：{2}</li><li>新闻图片：  <img src='{3}'/></li><li>新闻详情链接：<a href='{4}' target='_blank'>{4}</a></li></ul></li>";
                    foreach (articleList item in news.list)
                    {
                        i++;
                        strub.Append(string.Format(temp, "", item.article, item.source, item.icon, item.detailurl));
                    }
                    temp = "<h3>{0}</h3><ol>{1}</ol>";
                    returnStr = string.Format(temp, news.text, strub.ToString());
                    break;
                //菜谱类
                case "308000":
                    menuClass menu = response.ToObject<menuClass>();
                    strub = new StringBuilder();
                    i = 0;
                    temp = "<li>{0}<ul><li>菜名：{1}</li><li>菜谱信息：{2}</li><li>图片：<img src='{3}'</li><li>详情链接：<a href='{4}' target='_blank'>{4}</li></ul></li>";
                    foreach (var item in menu.list)
                    {
                        i++;
                        strub.Append(string.Format(temp, "", item.name, item.info, item.icon, item.detailurl));
                    }
                    temp = "<h3>{0}</h3><ol>{1}</ol>";
                    returnStr = string.Format(temp, menu.text, strub.ToString());
                    break;
                //儿歌类
                case "313000":
                    ChildrenIsSongs childrenIssongs = response.ToObject<ChildrenIsSongs>();
                    strub = new StringBuilder();
                    i = 0;
                    temp = "<li>{0}<ul><li>歌名：{1}</li><li>歌手：{2}</li></ul></li>";
                    foreach (var item in childrenIssongs.function)
                    {
                        i++;
                        strub.Append(string.Format(temp, "", item.song, item.singer));
                    }
                    temp = "<h3>{0}</h3><ol>{1}</ol>";
                    returnStr = string.Format(temp, childrenIssongs.text, strub.ToString());
                    break;
                //诗词类
                case "314000":
                    Poetry poetyr = response.ToObject<Poetry>();
                    strub = new StringBuilder();
                    i = 0;
                    temp = "<li>{0}<ul><li>作者：{1}</li><li>诗词名：{2}</li></ul></li>";
                    foreach (var item in poetyr.function)
                    {
                        i++;
                        strub.Append(string.Format(temp, "", item.author, item.name));
                    }
                    temp = "<h3>{0}</h3><ol>{1}</ol>";
                    returnStr = string.Format(temp, poetyr.text, strub.ToString());
                    break;
                #region 异常码
                case "40001": returnStr = "参数key错误"; break;
                case "40002": returnStr = "请求内容info为空"; break;
                case "40004": returnStr = "当天请求次数已使用完"; break;
                case "40007": returnStr = "数据格式异常"; break;
                #endregion
                //文字类 100000
                default:
                    turingText text = JsonConvert.DeserializeObject<turingText>(response);
                    returnStr = text.text;
                    if (returnStr.Contains("妖王"))
                        returnStr = text.text.Replace("妖王", "同学");
                    break;
            }
            return returnStr;
        }
    }
}
