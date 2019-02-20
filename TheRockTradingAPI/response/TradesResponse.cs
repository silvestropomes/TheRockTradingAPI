using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.response
{
    public class TradesResponse : IResponse
    {
        public IList<TradeResponse> Trades { get; set; }
    }
}
