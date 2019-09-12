using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

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
            var content = System.IO.File.ReadAllText(System.IO.Path.GetFullPath(@"Web\webPage.html"));

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html;charset=utf-8",               
            };
        }
    }
}
