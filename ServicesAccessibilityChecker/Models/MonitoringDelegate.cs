using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Models
{
    public class MonitoringDelegate : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Stopwatch watcher = Stopwatch.StartNew();
            using (HttpResponseMessage response = await base.SendAsync(request, cancellationToken))
            {
                watcher.Stop();
                response.Headers.Add("X-Duration", watcher.ElapsedMilliseconds.ToString());

                return response;
            }               
        }
    }
}
