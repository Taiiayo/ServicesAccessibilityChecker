using Quartz;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Scheduling
{
    public class StatusChecker : IJob
    {
        readonly string[] links = new string[] { "http://iswiftdata.1c-work.net/api/refdata/version",
                "http://ibonus.1c-work.net/api/ibonus/version",
                "http://iswiftdata.1c-work.net/api/catalog/catalog" };
        public async Task Execute(IJobExecutionContext context)
        {
            for (int i = 0; i < links.Length; i++)
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(links[i]),
                    Method = HttpMethod.Get,
                };
                if (i == 2)
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("accessKey=test_05fc5ed1-0199-4259-92a0-2cd58214b29c"));
                }
                Task task = httpClient.SendAsync(request)
                    .ContinueWith((taskwithmsg) =>
                    {
                        HttpResponseMessage response = taskwithmsg.Result;

                        Task<JsonObject> jsonTask = response.Content.ReadAsAsync<JsonObject>();
                        jsonTask.Wait();
                        JsonObject jsonObject = jsonTask.Result;
                    });
                task.Wait();
            }
        }
    }
}
