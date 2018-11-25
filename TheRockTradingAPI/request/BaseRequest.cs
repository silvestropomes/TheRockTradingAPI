using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.request
{
    public abstract class BaseRequest : IRequest
    {
        protected readonly ApiConfig apiConfig;

        internal abstract string Uri { get; }

        internal BaseRequest(ApiConfig apiConfig)
        {
            this.apiConfig = apiConfig;
        }

        public string GetUri()
        {
            return $"{this.apiConfig.BasePath}/{this.Uri}";
        }
    }
}
