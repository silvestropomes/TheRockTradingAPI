using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.restUtils
{
    internal class RateLimitedRestCaller : IRestCaller
    {
        private readonly RestCaller restCaller;
        private readonly ApiConfig apiConfig;

        public RateLimitedRestCaller(ApiConfig apiConfig, ThrottlingDelegatingHandler throttle)
        {
            HttpClient throttledClient = new HttpClient(throttle);
            this.apiConfig = apiConfig;
            this.restCaller = new RestCaller(apiConfig, throttledClient);
        }

        public T Get<T>(string uri) where T : IResponse
        {
            return this.restCaller.Get<T>(uri);
        }

        public Task<T> GetAsync<T>(string uri) where T : IResponse
        {
            return this.restCaller.GetAsync<T>(uri);
        }

        public T Post<T>(string uri, string postContent) where T : IResponse
        {
            return this.restCaller.Post<T>(uri, postContent);
        }

        public Task<T> PostAsync<T>(string uri, string postContent) where T : IResponse
        {
            return this.restCaller.PostAsync<T>(uri, postContent);
        }
    }
}
