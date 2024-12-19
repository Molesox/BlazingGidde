using Microsoft.AspNetCore.Mvc;

namespace BlazingGidde.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppCustomTemplatesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTemplates()
        {
            
            return Ok(new
            {
                success = true,
                items = new List<string>()
            });
        }
    }
}
