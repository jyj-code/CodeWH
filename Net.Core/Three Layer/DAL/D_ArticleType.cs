using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interface;
using Model;
using DatabaseOperation;
using System.Text;
using Common;

namespace DAL
{
      public class D_ArticleType : Interface.IDBOperating<Model.ArticleType>
    {
        public string Sql { get; set; }
        public int Add(List<ArticleType> _entity)
        {
            Sql = "INSERT INTO S_Role(ID,Name,Status,FirstID,Lover,Sequence,Remark,Link_Url)VALUES(@ID,@Name,@Status,@FirstID,@Lover,@Sequence,@Remark,@Link_Url)";
            return DBHelper.SaveCollection(Sql, _entity);
        }
        public int Delete(List<ArticleType> _entity)
        {
            Sql = "DELETE ArticleType WHERE ID=@ID";
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

        public List<ArticleType> Find()
        {
            Sql = " select distinct * from ArticleType order by Sequence desc";
            return DBHelper.ReadCollection<ArticleType>(Sql, null);
        }
        public List<ArticleType> GetTypeCount()
        {
            Sql = @"select a.*,B.ID as ID,B.Link_Url from (SELECT B.Name As Name,COUNT(B.Name) AS Lover
                  FROM ShortArticle A left join ArticleType B on A.ArticleType_ID = B.ID
                  group by B.Name,B.Link_Url) as a left join ArticleType B on a.Name = B.Name";
            return DBHelper.ReadCollection<ArticleType>(Sql, null);
        }
        public List<ArticleType> Find(string _sql)
        {
            return DBHelper.ReadCollection<ArticleType>(_sql, null);
        }
        public int Update(List<ArticleType> _entity)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in _entity)
            {
                if (StaticToolHelp.IsNullVar(item.Name))
                {
                    str.Append("Name=@Name,");
                }
                if (StaticToolHelp.IsNullVar(item.Status)&& item.Status==true)
                {
                    str.Append("Status=@Status,");
                }
                if (StaticToolHelp.IsNullVar(item.Sequence))
                {
                    str.Append("Sequence=@Sequence,");
                }
                if (StaticToolHelp.IsNullVar(item.Lover))
                {
                    str.Append("Lover=@Lover,");
                }
                if (StaticToolHelp.IsNullVar(item.Link_Url))
                {
                    str.Append("Link_Url=@Link_Url,");
                }
                if (StaticToolHelp.IsNullVar(item.FirstID))
                {
                    str.Append("FirstID=@FirstID,");
                }
                break;
            }
            Sql = String.Format("UPDATE ArticleType SET {0} WHERE ID=@ID", str.ToString().Substring(0, str.ToString().Length - 1));
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
