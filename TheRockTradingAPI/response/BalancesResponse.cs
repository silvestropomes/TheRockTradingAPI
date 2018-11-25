using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.response
{
    
    public class BalancesResponse : IResponse
    {
        internal BalancesResponse() { }

        public class BalanceResponse
        {
            public string Currency { get; set; }
            public decimal Balance { get; set; }
            [JsonProperty("trading_balance")]
            public decimal TradingBalance { get; set; }
        }

        public IEnumerable<BalanceResponse> Balances { get; set; }
    }
}
