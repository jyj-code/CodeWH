using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class B_ArticleLink : BaseBLL<Model.ArticleLink>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_ArticleLink();
        }
        public string GetArticleUrl(string id)
        {
            return new D_ArticleLink().GetArticleUrl(id);
        }
    }
}
