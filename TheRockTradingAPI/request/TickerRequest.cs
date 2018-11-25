using TheRockTradingAPI;
using TheRockTradingAPI.contract;
using TheRockTradingAPI.request;

namespace TheRockTradingAPI.request
{
    internal class TickerRequest : BaseRequest, IGetRequest
    {

        public TickerRequest(ApiConfig apiConfig) : base(apiConfig) { }

        internal string AssetPair { get; set; }

        internal override string Uri => $@"/v1/funds/{this.AssetPair}/ticker";
    }
}