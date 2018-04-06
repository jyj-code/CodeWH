using Dapper;
using DatabaseOperation;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class D_Article
    {
        public int PageCount { get; set; } = 10;
        public string Sql { get; set; }
        public List<Article> Find()
        {
            Sql = @"SELECT alert.ID,ArticleContent,alert.CustomerID,u.UserName,ArticleType_ID,t.Name As typeName,GetCount,Title,ReadCount,ThumbsUP,ArticleID.CommentCount,Linkalert.Link_Url_Url,alert.Template_ID,State,alert.CreateDate
                    FROM ShortArticle alert
                    Left join S_User u on u.ID = alert.CustomerID
                    left join ArticleType t on alert.ArticleType_ID=t.ID
                    Left join (select count(ArticleID) as CommentCount,ArticleID from comment pl group by ArticleID) ArticleID on ArticleID.ArticleID=alert.ID 	order by alert.CreateDate desc";
            return DBHelper.ReadCollection<Article>(Sql, null);
        }
        public Article GetArticle(string id)
        {
            Sql = @"SELECT alert.ID,ArticleContent,alert.CustomerID,u.UserName,ArticleType_ID,alert.Link_Url,alert.Template_ID,t.Name As typeName,GetCount,Title,ReadCount,ThumbsUP,ArticleID.CommentCount,State,alert.CreateDate
                    FROM ShortArticle alert
                    Left join S_User u on u.ID = alert.CustomerID
                    left join ArticleType t on alert.ArticleType_ID=t.ID
                    Left join (select count(ArticleID) as CommentCount,ArticleID from comment pl group by ArticleID) ArticleID on ArticleID.ArticleID=alert.ID where alert.ID='{0}'";
            Sql = string.Format(Sql,id);
            return DBHelper.ReadCollection<Article>(Sql, null).First();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<Article> Find(int PageIndex)
        {
            PageIndex = PageIndex * PageCount;
            switch (ConnService.DBTypeConnection)
            {
                case ConnService.DbType.SqlServer:
                    Sql = @"select top {0} * from ((select row_number() over(order by Article.CreateDate desc) as rownumber, * from (SELECT top 100 Percent alert.ID,ArticleContent,alert.CustomerID,u.UserName,ArticleType_ID,alert.Link_Url,alert.Template_ID,t.Name As typeName,GetCount,Title,ReadCount,ThumbsUP,ArticleID.CommentCount,State,alert.CreateDate
                    FROM ShortArticle alert
                    Left join S_User u on u.ID = alert.CustomerID
                    left join ArticleType t on alert.ArticleType_ID=t.ID
                    Left join (select count(ArticleID) as CommentCount,ArticleID from comment pl group by ArticleID) ArticleID on ArticleID.ArticleID=alert.ID 	
					order by alert.CreateDate desc) as Article)) as A
					where A.rownumber > @PageIndex";
                    Sql = string.Format(Sql, PageCount);
                    var p = new DynamicParameters();
                    p.Add("@PageIndex", PageIndex, System.Data.DbType.Int32);
                    return DBHelper.ReadCollection<Article>(Sql, p);
                default:
                    Sql = @"SELECT alert.ID,ArticleContent,alert.CustomerID,u.UserName,ArticleType_ID,alert.Link_Url,alert.Template_ID,t.Name As typeName,GetCount,Title,ReadCount,ThumbsUP,ArticleID.CommentCount,State,alert.CreateDate
                    FROM ShortArticle alert
                    Left join S_User u on u.ID = alert.CustomerID
                    left join ArticleType t on alert.ArticleType_ID=t.ID
                    Left join (select count(ArticleID) as CommentCount,ArticleID from comment pl group by ArticleID) ArticleID on ArticleID.ArticleID=alert.ID 	
					order by alert.CreateDate desc limit @PageCounts offset @PageIndex";
                    var p2 = new DynamicParameters();
                    p2.Add("@PageCounts", PageCount, System.Data.DbType.Int32);
                    p2.Add("@PageIndex", PageIndex, System.Data.DbType.Int32);
                    return DBHelper.ReadCollection<Article>(Sql, p2);
            }
            return null;
        }
        public int TotalPage()
        {
            Sql = @"SELECT count(1) FROM ShortArticle alert
                    Left join S_User u on u.ID = alert.CustomerID
                    left join ArticleType t on alert.ArticleType_ID=t.ID
                    Left join (select count(ArticleID) as CommentCount,ArticleID from comment pl group by ArticleID) ArticleID on ArticleID.ArticleID=alert.ID";
            var data = DBHelper.ExecuteScalar(Sql, null);
            return data % PageCount == 0 ? data % PageCount : (data % PageCount) + 1;
        }
        public List<Article> Find(string _sql)
        {
            return DBHelper.ReadCollection<Article>(_sql, null);
        }
    }
}
