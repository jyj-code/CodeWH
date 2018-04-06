using KebueAPI.RobotSDK;
using Microsoft.AspNetCore.Mvc;


namespace KebueAPI.Controllers
{
    [Route("api/[controller]")]
    public class RobotAPIController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            ApiS a = new ApiS();
            a.apikey = "ddc3e074ebbe2a7a01538056daa1c8ee";
            a.paraUrlCoded = "";

          //  HttpReques.HttpPost(url,);

            return Created($"api/users/{""}", "ShortArticle");
        }

    }
}
