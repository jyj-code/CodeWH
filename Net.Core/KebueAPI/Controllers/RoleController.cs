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
    public class RoleController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new B_S_Role().Find());
        }
        [HttpGet("{id}")]
        public IActionResult Get(S_Role id)
        {
            return Ok(new B_S_Role().Find());
        }
        [HttpPost]
        public IActionResult Post([FromBody]S_Role value)
        {
            return Created($"api/users/{""}", "Role");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]S_Role value)
        {
            return Created($"api/users/{""}", "Role");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(S_Role id)
        {
            if (id == null)
                return NotFound();
            return NoContent();
        }
    }
}
