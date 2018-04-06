using DAL;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class B_ShortArticle : BaseBLL<ShortArticle>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_ShortArticle();
        }
        public ShortArticle GetArticle(string id)
        {
            return new D_ShortArticle().GetArticle(id);
        }
        public int CreateArticle(ShortArticle article)
        {
            article.ID = System.Guid.NewGuid().ToString();
            article.CreateDate = System.DateTime.Now;
            article.GetCount = 1;
            article.State = new B_Sys_Config().GetStatus("appy_article");
           return Add(article);
        }
    }

}
