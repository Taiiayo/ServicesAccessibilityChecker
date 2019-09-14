using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicesAccessibilityChecker.Models;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Controllers
{
    // todo прокинуть url бэка во фронт
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IFullInfo _fullInfo;
        private readonly IStatusChecker _statusChecker;
        public StatusController(IFullInfo fullInfo, ILogger<StatusController> logger, IStatusChecker statusChecker)
        {
            _statusChecker = statusChecker;
            _fullInfo = fullInfo;
            _logger = logger;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<string>> GetStatusAsync(int serviceId)
        {
            //todo хардкод с айдишниками, надо исправить и передавать по-хорошему сразу ссылки, но пока не успела переписать логику
            if (serviceId > 2)
            {
                _logger.LogWarning($"Received Id of service: {serviceId} was greater than 2");
                return BadRequest("We cannot use IDs greater than 2.");
            }
            string serializedDto = await _statusChecker.SendRequestAsync(serviceId);
            if (string.IsNullOrEmpty(serializedDto))
            {
                return StatusCode(500);
            }
            else
            {
                return serializedDto;
            }
        }

        [HttpGet("fullStats")]
        public ActionResult<string> GetFullStatusAsync(int serviceId)
        {
            //todo хардкод с айдишниками, надо исправить и передавать по-хорошему сразу ссылки, но пока не успела переписать логику
            if (serviceId > 2)
            {
                _logger.LogWarning($"Received Id of service: {serviceId} was greater than 2");
                return BadRequest("We cannot use IDs greater than 2.");
            }

            string serializedDto = _fullInfo.ReturnFullInfo(serviceId);

            if (string.IsNullOrEmpty(serializedDto))
            {
                return StatusCode(500);
            }
            else
            {
                return serializedDto;
            }
        }
    }
}
