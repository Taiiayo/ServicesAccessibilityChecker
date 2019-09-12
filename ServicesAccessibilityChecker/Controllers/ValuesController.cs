using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesAccessibilityChecker.Models;
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
        private readonly FullInfo _fullInfo;
        public ValuesController(StatusChecker statusChecker, FullInfo fullInfo)
        {
            _statusChecker = statusChecker;
            _fullInfo = fullInfo;
        }

        [HttpGet("GetStatus")]
        public async Task<ActionResult<string>> GetStatusAsync(int serviceId)
        {
            StatusRm result = await _statusChecker.SendRequestAsync(serviceId);
            string ser = JsonConvert.SerializeObject(result);
            return ser;
        }

        [HttpGet("GetFullStatus")]
        public ActionResult<string> GetFullStatusAsync(int serviceId)
        {
            object result = _fullInfo.ReturnFullInfo(serviceId);
            string ser = JsonConvert.SerializeObject(result);
            return ser;
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
