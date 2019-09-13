using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicesAccessibilityChecker.Models;
using ServicesAccessibilityChecker.Scheduling;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<RootController> _logger;
        private readonly StatusChecker _statusChecker;
        private readonly FullInfo _fullInfo;
        public ValuesController(StatusChecker statusChecker, FullInfo fullInfo, ILogger<RootController> logger)
        {
            _statusChecker = statusChecker;
            _fullInfo = fullInfo;
            _logger = logger;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<string>> GetStatusAsync(int serviceId)
        {
            if (serviceId > 2)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("We cannot use IDs greater than 2.")
                };
                _logger.LogWarning($"Received Id of service: {serviceId} was greater than 2");
                throw new System.Web.Http.HttpResponseException(message);
            }
            string serializedDto = await _statusChecker.SendRequestAsync(serviceId);
            //if (string.IsNullOrEmpty(serializedDto))
            //{
            //    return StatusCode(500);
            //}
            //else
            {
                return serializedDto;               
            }
        }

        [HttpGet("fullStats")]
        public ActionResult<string> GetFullStatusAsync(int serviceId)
        {
            if (serviceId > 2)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("We cannot use IDs greater than 2.")
                };
                _logger.LogWarning($"Received Id of service: {serviceId} was greater than 2");
                throw new System.Web.Http.HttpResponseException(message);
            }
            string serializedDto = _fullInfo.ReturnFullInfo(serviceId);
            //if (string.IsNullOrEmpty(serializedDto))
            //{
            //    return StatusCode(500);
            //}
            //else
            {
                return serializedDto;
            }
        }
    }
}
