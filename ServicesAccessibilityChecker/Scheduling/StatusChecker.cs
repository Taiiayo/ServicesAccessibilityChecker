using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using ServicesAccessibilityChecker.Models.Rm;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Scheduling
{
    public class StatusChecker : IJob
    {
        private readonly ILogger<StatusChecker> _logger;
        private readonly Repository _repository;
        public StatusChecker(ILogger<StatusChecker> logger, Repository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        readonly string[] links = new string[]
        {
            "http://iswiftdata.1c-work.net/api/refdata/version",
            "http://ibonus.1c-work.net/api/ibonus/version",
            "http://iswiftdata.1c-work.net/api/catalog/catalog"
        };

        public async Task Execute(IJobExecutionContext context)
        {
            for (int i = 0; i < links.Length; i++)
            {
                await SendRequestAsync(i);
            }
        }

        public async Task<string> SendRequestAsync(int i)
        {
            RestClient client = new RestClient(links[i]);
            RestRequest request = new RestRequest(Method.GET);

            if (i == 2)
            {
                request.AddHeader("accessKey", "test_05fc5ed1-0199-4259-92a0-2cd58214b29c");
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
            return _repository.SaveResponse(i, stopWatch, response);
        }       
    }
}
