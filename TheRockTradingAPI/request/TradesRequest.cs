using Microsoft.AspNetCore.WebUtilities;
using System;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.request
{
    internal class TradesRequest : BaseRequest, IGetRequest
    {

        public TradesRequest(ApiConfig apiConfig) : base(apiConfig) { }

        internal string AssetPair { get; set; }
        internal int? TradeId { get; set; }
        internal int? Page { get; set; }
        internal int? PerPage { get; set; }
        internal DateTime? After { get; set; }
        internal DateTime? Before { get; set; }

        internal override string Uri => $@"v1/funds/{this.AssetPair}/trades{this.QueryString}";
        internal string QueryString => GetQueryString();

        private string GetQueryString()
        {
            var queryString = string.Empty;

            if (this.TradeId.HasValue) queryString = QueryHelpers.AddQueryString(queryString, "trade_id", this.TradeId.ToString());
            if (this.Page.HasValue) queryString = QueryHelpers.AddQueryString(queryString, "page", this.Page.ToString());
            if (this.PerPage.HasValue) queryString = QueryHelpers.AddQueryString(queryString, "per_page", this.PerPage.ToString());
            if (this.After.HasValue) queryString = QueryHelpers.AddQueryString(queryString, "after", this.After.Value.ToUniversalTime().ToString("O"));
            if (this.Before.HasValue) queryString = QueryHelpers.AddQueryString(queryString, "before", this.Before.Value.ToUniversalTime().ToString("O"));
            return queryString;
        }

    }
}