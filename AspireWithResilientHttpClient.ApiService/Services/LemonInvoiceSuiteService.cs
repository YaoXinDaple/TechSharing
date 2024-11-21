using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AspireWithResilientHttpClient.ApiService.Services
{
    public class LemonInvoiceSuiteService
    {
        private HttpClient _httpClient;
        private readonly ILogger<LemonInvoiceSuiteService> logger;

        public LemonInvoiceSuiteService(HttpClient httpClient, ILogger<LemonInvoiceSuiteService> logger)
        {
            _httpClient = httpClient;
            this.logger = logger;
        }

        public async Task ReadDataAsync()
        {
            var start = Stopwatch.GetTimestamp();
            try
            {
                var response = await _httpClient.GetAsync("api/app/company/tax-district-list");
                logger.LogInformation("loop 请求结束：200 - 耗时{ms}毫秒", Stopwatch.GetElapsedTime(start).TotalMilliseconds);
            }
            catch (Exception e)
            {
                logger.LogError(e,"loop 请求结束：{Status} - 耗时{ms}毫秒, Type:{Type}", (int)((e as HttpRequestException)?.StatusCode??0), Stopwatch.GetElapsedTime(start).TotalMilliseconds, e.GetType());
            }
        }
    }
}
