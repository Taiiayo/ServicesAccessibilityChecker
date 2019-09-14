using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Scheduling
{
    public class StatusChecker
    {
        private readonly IConfiguration _config;
        private readonly ILogger<StatusChecker> _logger;
        private readonly Repository _repository;

        public StatusChecker(ILogger<StatusChecker> logger, Repository repository, IConfiguration config)
        {
            _logger = logger;
            _repository = repository;
            _config = config;
        }

        public async Task<string> SendRequestAsync(int serviceId)
        {
            List<string> links = _config.GetSection("ServicesLinks:Links").Get<List<string>>();
            RestClient client = new RestClient(links[serviceId]);
            RestRequest request = new RestRequest(Method.GET);
            // вот тут 2 - явно магическое число, тут надо разделить ссылки на публичные и не публичные, но пока не придумала, как
            if (serviceId == 2)
            {
                request.AddHeader("accessKey", _config.GetSection("Headers:AccessKey").Get<string>());
            }

            IRestResponse response;
            Stopwatch stopWatch;
            try
            {
                stopWatch = Stopwatch.StartNew();
                response = await client.ExecuteTaskAsync(request);
                stopWatch.Stop();
            }
            catch
            {
                _logger.LogError("Couldn't execute the request");
                return string.Empty;
            }
            return _repository.SaveResponse(serviceId, stopWatch, response);
        }
    }
}
