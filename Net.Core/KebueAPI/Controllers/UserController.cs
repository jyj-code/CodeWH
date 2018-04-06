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
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new B_S_User().Find());
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(S_User id)
        {
            try
            {
                return Ok(new B_S_User().Find());
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]S_User value)
        {
            return Created($"api/users/{""}", "S_User");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]S_User value)
        {
            return Created($"api/users/{""}", "S_User");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(S_User id)
        {
            if (id == null)
                return NotFound();
            return NoContent();
        }
    }
}
