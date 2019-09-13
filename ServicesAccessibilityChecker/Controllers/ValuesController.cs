using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesAccessibilityChecker.Models;
using ServicesAccessibilityChecker.Models.Rm;
using ServicesAccessibilityChecker.Scheduling;
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
            StatusRm result = _fullInfo.ReturnFullInfo(serviceId);
            string ser = JsonConvert.SerializeObject(result);
            return ser;
        }
    }
}
