using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.common;

namespace TheRockTradingAPI.response
{
    public class TradeResponse
    {
        public int Id { get; set; }

        [JsonProperty("fund_id")]
        public string FundId { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSideEnum Side { get; set; }

        public bool Dark { get; set; }

        public DateTime Date { get; set; }
    }
}
