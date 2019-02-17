using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.response
{
    public class TickerResponse : IResponse
    {
        public DateTime Date { get; set; }

        [JsonProperty("fund_id")]
        public string FundId { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Last { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public decimal Volume { get; set; }
        [JsonProperty("volume_traded")]
        public decimal VolumeTraded { get; set; }
    }
}
