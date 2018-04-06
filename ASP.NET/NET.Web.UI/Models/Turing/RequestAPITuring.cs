using DotNet.Utilities;
using NET.Architect.Model;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace UI
{
    public class RequestAPITuring
    {



        /// <summary>
        ///  POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="session">参数</param>
        /// <returns></returns>
        public string HttpPost(string url, string param, UserInfoUnDeteil userinfo)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/json; charset=UTF-8";
            var model = new Models.Turing.Parameter(param, userinfo);
            string json = JsonConvert.SerializeObject(model);
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(json);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 图灵机器人请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetText(string session, UserInfoUnDeteil userin)
        {
            string result = string.Empty;
            #region 图灵机器人官网接口
            result = HttpPost(ConstParameter.Turing_url, session, userin);
            var res = JsonConvert.DeserializeObject<turingText>(result);
            if (res != null && res.code != null)
            {
                if (res.code == "40001" || res.code.Contains("40001") ||
                    res.code == "40004" || res.code.Contains("40004") ||
                    res.text == "当天请求次数已使用完" || res.text.Contains("天请求次数已使用完") ||
                    res.text == "参数key错误" || res.text.Contains("key错误"))
                {
                    switch (ConstParameter.Turing_key)
                    {
                        case "251cfdca8ce34d9c8f19ef1b437691cb":
                            ConstParameter.Turing_key = "4a61e57f11fa4b5f9a557ffec1d38359";
                            break;
                        case "4a61e57f11fa4b5f9a557ffec1d38359":
                            ConstParameter.Turing_key = "c4f3c14c5de44175be9b1b581cc7673c";
                            break;
                        case "c4f3c14c5de44175be9b1b581cc7673c":
                            ConstParameter.Turing_key = "c75ba576f50ddaa5fd2a87615d144ecf";
                            break;
                        case "c75ba576f50ddaa5fd2a87615d144ecf":
                            ConstParameter.Turing_key = "251cfdca8ce34d9c8f19ef1b437691cb";
                            break;
                            GetText(session, userin);
                    }
                }
                result = TuringAnswerText(res.code, result);
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 根据子节判读中英文
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private bool IsHanZi(string ch)
        {
            byte[] byte_len = System.Text.Encoding.Default.GetBytes(ch);
            if (byte_len.Length == 2) { return true; }
            return false;
        }
        /// <summary>
        /// 图灵回答文本
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public string TuringAnswerText(string code, string result)
        {
            int i = 0;
            StringBuilder strub;
            string temp = string.Empty;
            string returnStr = string.Empty;
            switch (code)
            {
                //链接 列出 航班类
                case "200000":
                    Links links = JsonConvert.DeserializeObject<Links>(result);
                    returnStr = string.Format("{0}<br /><a href='{1}' title='链接 列出 航班类  {2}' target='_blank'>{1}</a>", links.text, links.url, links.code);
                    break;
                //新闻类
                case "302000":
                    News news = JsonConvert.DeserializeObject<News>(result);

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
                    menuClass menu = JsonConvert.DeserializeObject<menuClass>(result);
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
                    ChildrenIsSongs childrenIssongs = JsonConvert.DeserializeObject<ChildrenIsSongs>(result);
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
                    Poetry poetyr = JsonConvert.DeserializeObject<Poetry>(result);
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
                    turingText text = JsonConvert.DeserializeObject<turingText>(result);
                    returnStr = text.text;
                    if (result.Contains("妖王"))
                        result = result.Replace("妖王", "同学");
                    break;
            }
            return returnStr;
        }

    }
}
