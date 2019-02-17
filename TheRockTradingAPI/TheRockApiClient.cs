using System;
using System.Security.Cryptography;
using System.Text;
using TheRockTradingAPI.contract;
using TheRockTradingAPI.request;
using TheRockTradingAPI.response;
using TheRockTradingAPI.restUtils;
using TheRockTradingAPI.extensions;
using System.Threading.Tasks;

namespace TheRockTradingAPI
{
    public class TheRockApiClient
    {
        private IServiceProvider serviceProvider = Bootstrapper.Bootstrap();
        private IRestCaller restCaller;

        public TheRockApiClient()
        {
            Initialize(string.Empty, string.Empty);
        }

        public TheRockApiClient(string privateKey, string apiKey)
        {
            Initialize(privateKey, apiKey);
        }

        private void Initialize(string privateKey, string apiKey)
        {
            var config = serviceProvider.GetService<ApiConfig>();
            if (!(String.IsNullOrEmpty(privateKey) || String.IsNullOrEmpty(apiKey)))
            {
                config.PrivateKey = privateKey;
                config.ApiKey = apiKey;
            }

            restCaller = serviceProvider.GetService<IRestCaller>();
        }

        public T Get<T>(IGetRequest request) where T : IResponse
        {
            return restCaller.Get<T>(request.GetUri());
        }

        public async Task<T> GetAsync<T>(IGetRequest request) where T : IResponse
        {
            return await restCaller.GetAsync<T>(request.GetUri());
        }

        public T Post<T>(IPostRequest request) where T : IResponse
        {
            return restCaller.Post<T>(request.GetUri(), request.GetPostContent());
        }

        public async Task<T> PostAsync<T>(IPostRequest request) where T : IResponse
        {
            return await restCaller.PostAsync<T>(request.GetUri(), request.GetPostContent());
        }

        public ITheRockRequestFactory TheRockRequestFactory => serviceProvider.GetService<ITheRockRequestFactory>();

    }
}
