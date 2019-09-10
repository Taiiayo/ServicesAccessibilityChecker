using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ServicesAccessibilityChecker.Models.Rm;
using ServicesAccessibilityChecker.Scheduling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Controllers
{
    [EnableCors("MyPolicy")]
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
        public async Task<StatusRm> GetStatusAsync(int serviceId)
        {
            var result = await _statusChecker.SendRequestAsync(serviceId);
            return result;
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
