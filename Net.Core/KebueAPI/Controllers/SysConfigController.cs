using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KebueAPI.Controllers
{
    [Route("api/[controller]")]
    public class SysConfigController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new B_Sys_Config().Find());
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(Sys_Config id)
        {
            return Ok("value");
        }
        [HttpPost]
        public IActionResult Post([FromBody]Sys_Config value)
        {
            return Created($"api/users/{""}", "Sys_Config");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Sys_Config value)
        {
            return Created($"api/users/{""}", "Sys_Config");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Sys_Config id)
        {
            return NoContent();
        }
    }
}
