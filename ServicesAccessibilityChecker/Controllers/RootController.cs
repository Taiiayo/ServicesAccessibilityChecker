using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ServicesAccessibilityChecker.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet]
        [HttpGet, Route("")]
        public IActionResult Get()
        {
            string content = System.IO.File.ReadAllText(System.IO.Path.GetFullPath(@"Web\webPage.html"));

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html;charset=utf-8",
            };
        }
    }
}
