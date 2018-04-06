using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KebueAPI.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        const int SizeLength = 500;
        B_Article BLL = new B_Article();
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                //stopwatch.Start();
                //var data = new B_Article().Find();
                //stopwatch.Stop();
                //string time = stopwatch.Elapsed.ToString();//	00:00:01.6949123
                return Ok(new B_Article().Find());
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                int PageIndex = 0;
                int.TryParse(id, out PageIndex);
                if (PageIndex > 0 || id == "0")
                {
                    var list = BLL.Find(PageIndex);
                    foreach (var item in list)
                    {
                        item.Month = item.CreateDate.ToString("MM") + "月";
                        item.Day = item.CreateDate.ToString("dd");
                        item.ArticleContent = StaticToolHelp.ReplaceHtmlTag(item.ArticleContent);
                        if (item.ArticleContent.Length > SizeLength)
                            item.ArticleContent = StaticToolHelp.GetSeparateSubString(item.ArticleContent, SizeLength);
                    }
                    return Ok(new { dataSource = list, PageSumCount = BLL.TotalPage() });
                }
                else
                {
                    switch (id)
                    {
                        case "type":
                            return Ok(new B_ArticleType().GetTypeCount());
                        default:
                            return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }

        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            return Created($"api/users/{""}", "ShortArticle");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return Created($"api/users/{""}", "ShortArticle");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null)
                return NotFound();
            return NoContent();
        }
    }
}
