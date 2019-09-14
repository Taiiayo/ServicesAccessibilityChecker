using Microsoft.Extensions.Configuration;
using Quartz;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Scheduling
{
    public class CheckStatusJob : IJob
    {
        private readonly IConfiguration _config;
        private readonly StatusChecker _statusChecker;

        public CheckStatusJob(
            IConfiguration config,
            StatusChecker statusChecker)
        {
            _config = config;
            _statusChecker = statusChecker;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<string> links = _config.GetSection("ServicesLinks:Links").Get<List<string>>();
            for (int i = 0; i < links.Count; i++)
            {
                await _statusChecker.SendRequestAsync(i);
            }
        }
    }
}
