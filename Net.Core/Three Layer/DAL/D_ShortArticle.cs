using Common;
using DatabaseOperation;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class D_ShortArticle : IDBOperating<ShortArticle>
    {
        public string Sql { get; set; }

        public int Add(List<ShortArticle> _entity)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder str2 = new StringBuilder();
            str.Append("Values(");
            str2.Append("(");
            foreach (var item in _entity)
            {
                if (StaticToolHelp.IsNullVar(item.ID))
                {
                    str.Append("@ID,");
                    str2.Append("ID,");
                }
                else
                    return 0;
                if (StaticToolHelp.IsNullVar(item.ArticleContent))
                {
                    str.Append("@ArticleContent,");
                    str2.Append("ArticleContent,");
                }
                else
                    return 0;
                if (StaticToolHelp.IsNullVar(item.CustomerID))
                {
                    str.Append("@CustomerID,");
                    str2.Append("CustomerID,");
                }
                else
                    return 0;
                if (StaticToolHelp.IsNullVar(item.ArticleType_ID))
                {
                    str.Append("@ArticleType_ID,");
                    str2.Append("ArticleType_ID,");
                }
                else
                    return 0;
                if (StaticToolHelp.IsNullVar(item.Title))
                {
                    str.Append("@Title,");
                    str2.Append("Title,");
                }
                else
                    return 0;
                if (StaticToolHelp.IsNullVar(item.GetCount))
                {
                    str.Append("@GetCount,");
                    str2.Append("GetCount,");
                }
                if (StaticToolHelp.IsNullVar(item.CreateDate))
                {
                    str.Append("@CreateDate,");
                    str2.Append("CreateDate,");
                }
                if (StaticToolHelp.IsNullVar(item.State))
                {
                    str.Append("@State,");
                    str2.Append("State,");
                }
                if (StaticToolHelp.IsNullVar(item.Link_Url))
                {
                    str.Append("@Link_Url,");
                    str2.Append("Link_Url,");
                }
                if (StaticToolHelp.IsNullVar(item.Template_ID))
                {
                    str.Append("@Template_ID,");
                    str2.Append("Template_ID,");
                }
                if (StaticToolHelp.IsNullVar(item.TagKeyWords))
                {
                    str.Append("@TagKeyWords,");
                    str2.Append("TagKeyWords,");
                }
            }
            Sql = String.Format("INSERT INTO ShortArticle {0}) {1})", str2.ToString().Substring(0, str2.ToString().Length - 1), str.ToString().Substring(0, str.ToString().Length - 1));
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<ShortArticle> _entity)
        {
            Sql = "DELETE ShortArticle WHERE ID=@ID";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Excute(string _sql)
        {
            return DBHelper.ExcuteSQL(_sql, null);
        }

        public string ExecuteScalarString(string _sql)
        {
            return DBHelper.ExecuteScalarString(_sql, null);
        }

        public List<ShortArticle> Find()
        {
            Sql = "select * from ShortArticle order by CreateDate desc";
            return DBHelper.ReadCollection<ShortArticle>(Sql, null);
        }

        public List<ShortArticle> Find(string _sql)
        {
            return DBHelper.ReadCollection<ShortArticle>(_sql, null);
        }
        public ShortArticle GetArticle(string id)
        {
            return DBHelper.ReadCollection<ShortArticle>(string.Format("select * from ShortArticle where id = '{0}", id), null).First();
        }

        public int Update(List<ShortArticle> _entity)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in _entity)
            {
                if (StaticToolHelp.IsNullVar(item.ArticleContent))
                {
                    str.Append("ArticleContent=@ArticleContent,");
                }
                if (StaticToolHelp.IsNullVar(item.CustomerID))
                {
                    str.Append("CustomerID=@CustomerID,");
                }
                if (StaticToolHelp.IsNullVar(item.ArticleType_ID))
                {
                    str.Append("ArticleType_ID=@ArticleType_ID,");
                }
                if (StaticToolHelp.IsNullVar(item.CustomerID))
                {
                    str.Append("CustomerID=@CustomerID,");
                }
                if (StaticToolHelp.IsNullVar(item.GetCount))
                {
                    str.Append("GetCount=@GetCount,");
                }
                if (StaticToolHelp.IsNullVar(item.Title))
                {
                    str.Append("Title=@Title,");
                }
                if (StaticToolHelp.IsNullVar(item.State))
                {
                    str.Append("State=@State,");
                }
                if (StaticToolHelp.IsNullVar(item.Link_Url))
                {
                    str.Append("Link_Url=@Link_Url,");
                }
                if (StaticToolHelp.IsNullVar(item.Template_ID))
                {
                    str.Append("Template_ID=@Template_ID,");
                }
                break;
            }
            Sql = String.Format("UPDATE ShortArticle SET {0} WHERE ID=@ID", str.ToString().Substring(0, str.ToString().Length - 1));
            return DBHelper.SaveCollection(Sql, _entity);
        }

    }
}
