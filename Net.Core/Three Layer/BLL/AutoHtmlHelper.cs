using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutoHtmlHelper
    {
        public static string Path { get; set; }
        public static string TemplateID { get; set; }
        public static string TemplateContent { get; set; }
        public static string RoutingPath(string user, string Article_ID)
        {
            return string.Format("Artlcle/{0}/{1}.html", user, Article_ID);
        }

        public static string M1 { get { return "[M_1]"; } }
        public static string M2 { get { return "[M_2]"; } }
        public static string M3 { get { return "[M_3]"; } }
        public static string M4 { get { return "[M_4]"; } }
        public static string M5 { get { return "[M_5]"; } }
        public static string M6 { get { return "[M_6]"; } }
        public string Temlplate { get; set; }
        /// <summary>
        /// 文章全部重新生成静态页面
        /// </summary>
        public static void AutoCreateArticle()
        {
            foreach (var item in new B_ShortArticle().Find())
            {
                var userObj = new B_S_User().Find().Where(n => n.ID == item.CustomerID).ToList();
                if (userObj == null || userObj.Count <= 0)
                    continue;
                if (string.IsNullOrEmpty(item.ID) || string.IsNullOrEmpty(userObj.First().Template_ID) || string.IsNullOrEmpty(userObj.First().UserAccount))
                    continue;
                ImportTemlplate(userObj.First().Template_ID, item.ID, userObj.First().UserAccount);
            }
        }
        /// <summary>
        /// 文章类型生成静态页面
        /// </summary>
        /// <returns></returns>
        public static bool AutoCreateArticleType()
        {
            StringBuilder str = new StringBuilder();
            TemplateID = "9F6DC9EC-7DD5-414E-9D8F-78D640A62787";
            TemplateContent = new B_Template().GetTemplate(TemplateID);
            List<ArticleType> list = new List<ArticleType>();
            string li = "<li><span>{0}</span>&nbsp;&nbsp;<a class='btn btn-link' style='color: blue' href='../../../../../{1}' title='{2}'>{3}</a>&nbsp;&nbsp;&nbsp;&nbsp;<span>{4}</span></li>";
            foreach (var articleType in new B_ArticleType().Find())
            {
                int i = 0;
                ArticleType at = new ArticleType();
                at.ID = articleType.ID;
                at.Link_Url = RoutingPath("ArticleType", articleType.ID);
                Path = string.Format("{0}/wwwroot/{1}", System.IO.Directory.GetCurrentDirectory(), at.Link_Url);
                foreach (var item in new B_ShortArticle().Find().Where(n => n.ArticleType_ID == articleType.ID))
                {
                    i++;
                    if (string.IsNullOrEmpty(item.ID) || string.IsNullOrEmpty(item.Link_Url) || string.IsNullOrEmpty(item.Title))
                        continue;
                    str.Append(string.Format(li, i, item.Link_Url, StaticToolHelp.ReplaceHtmlTag(item.Title), item.Title, item.CreateDate));
                }
                string Content = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M1, articleType.Name));
                Content = StaticToolHelp.StringConvertGB2312(Content.Replace(M2, articleType.Name));
                Content = StaticToolHelp.StringConvertGB2312(Content.Replace(M3, articleType.Name));
                Content = StaticToolHelp.StringConvertGB2312(Content.Replace(M4, str.ToString()));
                InputTemplate(Content, Path);
                list.Add(at);
                str.Length = 0;
            }
            new B_ArticleType().Update(list);
            return true;
        }
        public static bool AutoCreateArticleType(string typeID)
        {
            StringBuilder str = new StringBuilder();
            TemplateID = "9F6DC9EC-7DD5-414E-9D8F-78D640A62787";
            TemplateContent = new B_Template().GetTemplate(TemplateID);
            List<ArticleType> list = new List<ArticleType>();
            string li = "<li><span>{0}</span>&nbsp;&nbsp;<a class='btn btn-link' style='color: blue' href='../../../../../{1}' title='{2}'>{3}</a>&nbsp;&nbsp;&nbsp;&nbsp;<span>{4}</span></li>";
            foreach (var articleType in new B_ArticleType().Find().Where(n => n.ID == typeID))
            {
                int i = 0;
                ArticleType at = new ArticleType();
                at.ID = articleType.ID;
                at.Link_Url = RoutingPath("ArticleType", articleType.ID);
                Path = string.Format("{0}/wwwroot/{1}", System.IO.Directory.GetCurrentDirectory(), at.Link_Url);
                foreach (var item in new B_ShortArticle().Find().Where(n => n.ArticleType_ID == articleType.ID))
                {
                    i++;
                    if (string.IsNullOrEmpty(item.ID) || string.IsNullOrEmpty(item.Link_Url) || string.IsNullOrEmpty(item.Title))
                        continue;
                    str.Append(string.Format(li, i, item.Link_Url, StaticToolHelp.ReplaceHtmlTag(item.Title), item.Title, item.CreateDate));
                }
                string Content = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M1, articleType.Name));
                Content = StaticToolHelp.StringConvertGB2312(Content.Replace(M2, articleType.Name));
                Content = StaticToolHelp.StringConvertGB2312(Content.Replace(M3, articleType.Name));
                Content = StaticToolHelp.StringConvertGB2312(Content.Replace(M4, str.ToString()));
                InputTemplate(Content, Path);
                list.Add(at);
                str.Length = 0;
            }
            new B_ArticleType().Update(list);
            return true;
        }
        /// <summary>
        /// 页面生成静态页面
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="Article_ID"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool ImportTemlplate(string templateID, string Article_ID, string user)
        {
            if (string.IsNullOrEmpty(templateID) || string.IsNullOrEmpty(Article_ID) || string.IsNullOrEmpty(user))
                return false;
            if (user.Contains("@"))
                user = user.Substring(0, user.LastIndexOf('@'));
            B_ShortArticle articleBll = new B_ShortArticle();
            ShortArticle entity = new ShortArticle();
            B_Article bll = new B_Article();
            entity.Link_Url = RoutingPath(user, Article_ID);
            entity.Template_ID = templateID;
            entity.ID = Article_ID;
            TemplateContent = new B_Template().GetTemplate(entity.Template_ID);
            var ArticleContent = bll.GetArticle(Article_ID);
            Path = string.Format("{0}/wwwroot/{1}", System.IO.Directory.GetCurrentDirectory(), entity.Link_Url);
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M1, ArticleContent.Title));
            string str = StaticToolHelp.StringConvertGB2312(StaticToolHelp.ReplaceHtmlTag(ArticleContent.ArticleContent));
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M2, str.Length > 200 ? str.Substring(0, 200) : str));
            var ArticleType = new B_ArticleType().Find();
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M3, string.Format("<a href='../../../../../{0}' title='{1}'>{1}</a>", ArticleType.Where(n => n.ID == ArticleContent.ArticleType_ID).First().Link_Url, ArticleContent.typeName)));
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M4, ArticleContent.ArticleContent));
            InputTemplate(TemplateContent, Path);
            new B_ShortArticle().Update(entity);
            return true;
        }


        public static string CreateArticleImportTemlplate(string templateID, string Article_ID, string user)
        {
            if (string.IsNullOrEmpty(templateID) || string.IsNullOrEmpty(Article_ID) || string.IsNullOrEmpty(user))
                return null;
            if (user.Contains("@"))
                user = user.Substring(0, user.LastIndexOf('@'));
            B_ShortArticle articleBll = new B_ShortArticle();
            ShortArticle entity = new ShortArticle();
            B_Article bll = new B_Article();
            entity.Link_Url = RoutingPath(user, Article_ID);
            entity.Template_ID = templateID;
            entity.ID = Article_ID;
            TemplateContent = new B_Template().GetTemplate(entity.Template_ID);
            var ArticleContent = bll.GetArticle(Article_ID);
            Path = string.Format("{0}/wwwroot/{1}", System.IO.Directory.GetCurrentDirectory(), entity.Link_Url);
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M1, ArticleContent.Title));
            string str = StaticToolHelp.StringConvertGB2312(StaticToolHelp.ReplaceHtmlTag(ArticleContent.ArticleContent));
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M2, str.Length > 200 ? str.Substring(0, 200) : str));
            var ArticleType = new B_ArticleType().Find();
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M3, string.Format("<a href='../../../../../{0}' title='{1}'>{1}</a>", ArticleType.Where(n => n.ID == ArticleContent.ArticleType_ID).First().Link_Url, ArticleContent.typeName)));
            TemplateContent = StaticToolHelp.StringConvertGB2312(TemplateContent.Replace(M4, ArticleContent.ArticleContent));
            InputTemplate(TemplateContent, Path);
            return entity.Link_Url;
        }
        /// <summary>
        /// 写入模板
        /// </summary>
        /// <param name="uaer"></param>
        /// <param name="Content"></param>
        public static void InputTemplate(string Content, string path)
        {
            string str = path.Substring(0, path.LastIndexOf('/'));
            if (!Directory.Exists(str))
                Directory.CreateDirectory(str);
            WriteImport(Content, path);
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Content"></param>
        public static void WriteImport(string Content, string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Create);
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(Content);
            fs.Write(data, 0, data.Length);
            fs.Flush();
        }
    }
}
