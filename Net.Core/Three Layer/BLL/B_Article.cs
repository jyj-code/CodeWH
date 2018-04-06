using DAL;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class B_Article
    {
        D_Article DAL = new D_Article();
        public string Sql { get; set; }
        public List<Article> Find()
        {
            return DAL.Find();
        }
        public List<Article> Find(int PageIndex)
        {
            return DAL.Find(PageIndex);
        }
        public Article GetArticle(string id)
        {
            return DAL.GetArticle(id);
        }
        public List<Article> Find(string _sql)
        {
            return DAL.Find(_sql);
        }
        public int TotalPage()
        {
            return DAL.TotalPage();
        }
    }
}
