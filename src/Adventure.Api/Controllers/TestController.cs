using Microsoft.AspNetCore.Mvc;

namespace Adventure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Hello World!");
        }
    }
}
