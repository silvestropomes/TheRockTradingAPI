using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.request
{
    internal class BalancesRequest : BaseRequest, IGetRequest
    {
        internal override string Uri => @"v1/balances";

        public BalancesRequest(ApiConfig apiConfig) : base(apiConfig)
        {
        }

    }

}
