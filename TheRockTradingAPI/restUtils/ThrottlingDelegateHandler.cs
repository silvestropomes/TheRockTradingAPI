using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TheRockTradingAPI.restUtils
{
    internal class ThrottlingDelegatingHandler : DelegatingHandler
    {
        private SemaphoreSlim _throttler;
        private readonly ApiConfig apiConfig;

        public ThrottlingDelegatingHandler(SemaphoreSlim throttler, ApiConfig apiConfig)
        {
            _throttler = throttler ?? throw new ArgumentNullException(nameof(throttler));
            this.apiConfig = apiConfig;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await _throttler.WaitAsync(cancellationToken);
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(apiConfig.RestDelayMilliseconds), cancellationToken);
                return await base.SendAsync(request, cancellationToken);
            }
            finally
            {
                _throttler.Release();
            }
        }
    }
}
