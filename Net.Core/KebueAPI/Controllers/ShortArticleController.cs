using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KebueAPI.Controllers
{
    [Route("api/[controller]")]
    public class ShortArticleController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new B_ShortArticle().Find());
            }
            catch (Exception ex)
            {
               return NotFound(ex.ToString());
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(ShortArticle id)
        {
            try
            {
                return Ok(new B_ShortArticle().Find());
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }
        [HttpPost]
        public IActionResult Post(ShortArticle article)
        {
            B_ShortArticle BLL=new B_ShortArticle();
            int flage = 0;
            try
            {
                if (!string.IsNullOrEmpty(article.CustomerID))
                {
                    bool IsArticleType_ID = false;//用作是否新增类型
                    var CurrentUserObj = new B_S_User().GetUserObj(article.CustomerID);
                    article.CustomerID = CurrentUserObj.ID;
                    article.Template_ID = string.IsNullOrEmpty(CurrentUserObj.Template_ID) ? "9F6DC9EC-7DD5-414E-9D8F-78D640A62787" : CurrentUserObj.Template_ID;
                    article.Link_Url = AutoHtmlHelper.CreateArticleImportTemlplate(article.Template_ID, article.ID, CurrentUserObj.UserAccount);
                    if (string.IsNullOrEmpty(new B_ArticleType().GetArticleID(article.ArticleType_ID)))
                    {
                        article.ArticleType_ID = System.Guid.NewGuid().ToString();
                        IsArticleType_ID = true;
                    }
                    flage = BLL.CreateArticle(article);
                    if (IsArticleType_ID && flage > 0)
                    {
                        ArticleType t = new ArticleType();
                        t.Remark = "customize";
                        t.ID = article.ArticleType_ID;
                        t.Name = article.ArticleType_ID;
                        t.Link_Url = new B_ArticleType().GetArticleLink_Url("other");
                        t.Status = true;
                        t.Lover = 3;
                        new B_ArticleType().Add(t);
                        AutoHtmlHelper.AutoCreateArticleType("other");
                    }
                    else if (flage > 0&& IsArticleType_ID==false)
                    {
                        Dispose();
                        GC.Collect();                    
                        AutoHtmlHelper.AutoCreateArticleType(article.ArticleType_ID);
                    }

                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
            return Ok(flage);
        }
        [HttpPut]
        public IActionResult Put(ShortArticle value)
        {
            return Created($"api/users/{""}", "ShortArticle");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(ShortArticle id)
        {
            if (id == null)
                  return NotFound();
                return NoContent();
        }
    
    }
}
