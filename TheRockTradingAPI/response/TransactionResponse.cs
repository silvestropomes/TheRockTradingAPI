using Newtonsoft.Json;
using System;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.response
{
    public class TransactionResponse : IResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }

        [JsonProperty("fund_id")]
        public string FundId { get; set; }

        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        [JsonProperty("trade_id")]
        public int TradeId { get; set; }

        [JsonProperty("transfer_detail")]
        public TransferDetail TransferDetail { get; set; }

    }


    public class TransferDetail
    {
        public string Method { get; set; }
        public string Id { get; set; }
        public string Recipient { get; set; }
        public string Confirmation { get; set; }
    }
}