using BLL;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class B_ArticleType : BaseBLL<ArticleType>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_ArticleType();
        }
        public List<ArticleType> GetTypeCount()
        {
            return new D_ArticleType().GetTypeCount();
        }
        public string GetArticleID(string TypeName)
        {
            string id = string.Empty;
            try
            {
                id = Find().Where(n => n.Name == TypeName).First().ID;
            }
            catch (Exception)
            {
                return null;
            }
            return string.IsNullOrEmpty(id) ? null : id; ;
        }
        public string GetArticleLink_Url(string ID)
        {
            string id = string.Empty;
            try
            {
                id = Find().Where(n => n.ID == ID).First().Link_Url;
            }
            catch (Exception)
            {
                return null;
            }
            return string.IsNullOrEmpty(id) ? null : id; ;
        }
    }
}
