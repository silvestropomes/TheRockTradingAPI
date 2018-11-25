using System;
using System.Security.Cryptography;
using System.Text;
using TheRockTradingAPI.contract;
using TheRockTradingAPI.request;
using TheRockTradingAPI.response;
using TheRockTradingAPI.restUtils;
using TheRockTradingAPI.extensions;

namespace TheRockTradingAPI
{
    public class TheRockApiClient
    {
        private IServiceProvider serviceProvider = Bootstrapper.Bootstrap();
        private RestCaller restCaller;

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
            if(!(String.IsNullOrEmpty(privateKey) || String.IsNullOrEmpty(apiKey)) ) {
                config.PrivateKey = privateKey;
                config.ApiKey = apiKey;
            }

            restCaller = serviceProvider.GetService<RestCaller>();
        }

        public BalancesResponse Get<BalancesResponse>(IGetRequest request)
        {
            return restCaller.Get<BalancesResponse>(request.GetUri());
        }

        public ITheRockRequestFactory TheRockRequestFactory => serviceProvider.GetService<ITheRockRequestFactory>();

    }
}
