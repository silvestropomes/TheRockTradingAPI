using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.common;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.response
{
    public class OrderResponse : IResponse
    {
        public decimal Id { get; set; }

        [JsonProperty("fund_id")]
        public string FundId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSideEnum Side { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderTypeEnum Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatusEnum Status { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        [JsonProperty("amount_unfilled")]
        public decimal AmountUnfilled { get; set; }

        [JsonProperty("conditional_type")]
        public OrderConditionalTypeEnum? ConditionalType { get; set; }

        [JsonProperty("conditional_price")]
        public decimal? ConditionalPrice { get; set; }

        public DateTime Date { get; set; }

        [JsonProperty("close_on")]
        public DateTime? CloseOn { get; set; }

        public bool Dark { get; set; }

        public decimal Leverage { get; set; }

        [JsonProperty("position_id")]
        public int? PositionId { get; set; }

        public IList<TradeResponse> Trades { get; set; }

        /*{
  "id": 397059460,
  "fund_id": "LTCEUR",
  "side": "buy",
  "type": "limit",
  "status": "active",
  "price": 20,
  "amount": 0.01,
  "amount_unfilled": 0.01,
  "conditional_type": null,
  "conditional_price": null,
  "date": "2019-02-06T17:42:11.490Z",
  "close_on": null,
  "dark": false,
  "leverage": 1,
  "position_id": null,
  "trades": []
}*/
    }
}
