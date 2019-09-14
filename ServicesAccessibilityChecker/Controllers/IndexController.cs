using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServicesAccessibilityChecker.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly ILogger<IndexController> _logger;
        public IndexController(ILogger<IndexController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [HttpGet, Route("")]
        public IActionResult Get()
        {
            string content = System.IO.File.ReadAllText(System.IO.Path.GetFullPath(@"Web\index.html"));
            try
            {
                return new ContentResult()
                {
                    Content = content,
                    ContentType = "text/html;charset=utf-8",
                };
            }
            catch
            {
                _logger.LogError("Wasn't able to load the webPage, please, check the source");
                return NotFound("We cannot find the page.");
            }
        }
    }
}
