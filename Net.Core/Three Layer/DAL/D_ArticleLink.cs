using DatabaseOperation;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class D_ArticleLink : Interface.IDBOperating<Model.ArticleLink>
    {
        public string Sql { get; set; }

        public int Add(List<ArticleLink> _entity)
        {
            Sql = @"insert into ArticleLink(ID ,Link_Url,Template_ID ,DATE)values(@ID ,@Link_Url,@Template_ID ,@DATE)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<ArticleLink> _entity)
        {
            Sql = "DELETE ArticleLink WHERE NAME=@NAME and ID=@ID";
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
        public string GetArticleUrl(string id)
        {
            return DBHelper.ExecuteScalarString(string.Format("select Template_ID from ArticleLink where ID='{0}'", id), null);
        }
        public List<ArticleLink> Find()
        {
            Sql = "select * from ArticleLink order by DATE";
            return DBHelper.ReadCollection<ArticleLink>(Sql, null);
        }

        public List<ArticleLink> Find(string _sql)
        {
            return DBHelper.ReadCollection<ArticleLink>(_sql, null);
        }

        public int Update(List<ArticleLink> _entity)
        {
            Sql = "UPDATE ArticleLink SET Link_Url=@Link_Url,Template_ID=@Template_ID,DATE=@DATE where ID=@ID";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
