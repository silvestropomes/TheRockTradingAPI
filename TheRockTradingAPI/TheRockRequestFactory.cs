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
            var req = serviceProvider.GetService<TickerRequest>();
            req.AssetPair = assetPair;
            return req;
        }

        public IPostRequest GetPlaceBuyOrderRequest(string assetPair, decimal amount, decimal price)
        {
            var req = serviceProvider.GetService<PlaceOrderRequest>();
            req.Amount = amount;
            req.Price = price;
            req.AssetPair = assetPair;
            req.Side = common.OrderSideEnum.buy;

            return req;
        }
        
        public IPostRequest GetPlaceSellOrderRequest(string assetPair, decimal amount, decimal price)
        {
            var req = serviceProvider.GetService<PlaceOrderRequest>();
            req.Amount = amount;
            req.Price = price;
            req.AssetPair = assetPair;
            req.Side = common.OrderSideEnum.sell;

            return req;
        }

    }
}
