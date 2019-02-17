using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using TheRockTradingAPI.contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TheRockTradingAPI.common;

namespace TheRockTradingAPI.request
{
    public class PlaceOrderRequest : BaseRequest, IPostRequest
    {
        public PlaceOrderRequest(ApiConfig apiConfig) : base(apiConfig)
        {
        }

        internal override string Uri => $"v1/funds/{this.AssetPair}/orders";

        [JsonProperty("fund_id")]
        internal string AssetPair { get; set; }

        [JsonProperty("side")]
        [JsonConverter(typeof(StringEnumConverter))]
        internal OrderSideEnum Side { get; set; }

        [JsonProperty("amount")]
        internal decimal Amount { get; set; }

        [JsonProperty("price")]
        internal decimal Price { get; set; }

        public string GetPostContent()
        {
            return JsonConvert.SerializeObject(this);
        }

       
    }
}
