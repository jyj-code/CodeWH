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
    public class MenuController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new B_S_Menu().Find());
        }
        [HttpGet("{id}")]
        public IActionResult Get(S_Menu id)
        {
            return Ok(new B_ShortArticle().Find());
        }
        [HttpPost]
        public IActionResult Post([FromBody]S_Menu value)
        {
            return Created($"api/users/{""}", "ShortArticle");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ShortArticle value)
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
