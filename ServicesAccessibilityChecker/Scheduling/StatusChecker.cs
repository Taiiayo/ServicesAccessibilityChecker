using Quartz;
using RestSharp;
using ServicesAccessibilityChecker.Models.Rm;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Scheduling
{
    public class StatusChecker : IJob
    {
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

        public async Task<StatusRm> SendRequestAsync(int i)
        {
            var client = new RestClient(links[i]);
            var request = new RestRequest(Method.GET);

            if (i == 2)
            {
                request.AddHeader("accessKey", "test_05fc5ed1-0199-4259-92a0-2cd58214b29c");
            }

            Stopwatch stopWatch = Stopwatch.StartNew();
            IRestResponse response = await client.ExecuteTaskAsync(request);
            stopWatch.Stop();

            Repository repository = new Repository();
            repository.AddToDb(i, response, stopWatch);

            return new StatusRm
            {
                IsAvailable = response.IsSuccessful,
                ServiceName = response.Content,
                ResponseDuration = stopWatch.ElapsedMilliseconds
            };
        }
    }
}
