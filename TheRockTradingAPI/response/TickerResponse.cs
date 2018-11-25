using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.response
{
    public class TickerResponse : IResponse
    {
        public DateTime Date { get; set; }
        public string Fund_id { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Last { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public decimal Volume { get; set; }
        public decimal Volume_traded { get; set; }
    }
}
