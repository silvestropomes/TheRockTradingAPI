using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;
using TheRockTradingAPI.request;
using TheRockTradingAPI.extensions;

namespace TheRockTradingAPI
{
    public class TheRockRequestFactory : ITheRockRequestFactory
    {
        private IServiceProvider serviceProvider;

        internal TheRockRequestFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IGetRequest GetBalancesRequest()
        {
            return serviceProvider.GetService<BalancesRequest>();
        }

        public IGetRequest GetTickerRequest(string assetPair)
        {
            var req =  serviceProvider.GetService<TickerRequest>();
            req.AssetPair = assetPair;
            return req;
        }
    }
}
