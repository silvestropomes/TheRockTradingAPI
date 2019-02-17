using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.contract
{
    public interface ITheRockRequestFactory
    {
        IGetRequest GetBalancesRequest();
        IGetRequest GetTickerRequest(string assetPair);

        IPostRequest GetPlaceSellOrderRequest(string assetPair, decimal amount, decimal price);
        IPostRequest GetPlaceBuyOrderRequest(string assetPair, decimal amount, decimal price);
        
    }
}
