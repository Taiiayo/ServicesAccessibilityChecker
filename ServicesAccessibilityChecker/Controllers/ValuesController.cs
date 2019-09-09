using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAccessibilityChecker.Scheduling;

namespace ServicesAccessibilityChecker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly StatusChecker _statusChecker;
        public ValuesController(StatusChecker statusChecker)
        {
            _statusChecker = statusChecker;
        }

        [HttpGet("GetStatus")]
        public async Task<IActionResult> GetStatusAsync(int serviceId)
        {
            if (serviceId == 0)
            {

            }
            else if (serviceId == 1)
            {

            }
            else
            {

            }
            await _statusChecker.SendRequestAsync(serviceId);
            return Ok("");
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
