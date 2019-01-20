using BrakePedal;
using BrakePedal.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.restUtils
{
    internal class BrakedRestCaller : IRestCaller, IDisposable
    {
        private readonly RestCaller restCaller;
        
        public BrakedRestCaller(ApiConfig apiConfig, IThrottleRepository throttleRepository)
        {
            var apiRequestPolicy = new HttpThrottlePolicy<IpRequestKey>(throttleRepository)
            {
                Name = "Requests",
                Prefixes = new[] { "requests" },

                PerSecond = 1 // Only allow 10 requests per second per IP
            };

            DelegatingHandler throttleHandler = new ThrottleHandler(apiRequestPolicy) { InnerHandler = new HttpClientHandler() };

            var httpClient = new HttpClient(throttleHandler);
            this.restCaller = new RestCaller(apiConfig, httpClient);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string uri) where T : IResponse
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string uri) where T : IResponse
        {
            throw new NotImplementedException();
        }

        
    }

    public class IpRequestKey : HttpRequestKey
    {
        public override void Initialize(HttpRequestMessage request)
        {
            base.Initialize(request);

            string forwardedFor = "X-Forwarded-For";
            if (request.Headers.Contains(forwardedFor))
            {
                // Use the forwarded IP address if sitting behind a load balancer
                string ip = request.Headers.GetValues(forwardedFor).First().Trim();

                // The base class HttpRequestKey gets the IP from other sources.
                ClientIp = IPAddress.Parse(ip);
            }
        }

        public override object[] Values
        {
            get
            {
                return new object[]
                {
                ClientIp
                };
            }
        }
    }
}
