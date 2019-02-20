using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;
using Newtonsoft.Json;
using TheRockTradingAPI.restUtils;
using TheRockTradingAPI.request;
using TheRockTradingAPI.contract;
using System.IO;
using System.Net.Http;

namespace TheRockTradingAPI
{
    
    internal class Bootstrapper
    {

        public static IServiceProvider Bootstrap()
        {
            var container = new Container();
            container.Register<ApiConfig>(Lifestyle.Singleton);
            container.Register<RestCaller>(Lifestyle.Singleton);
            
            container.Register<IRestCaller, RateLimitedRestCaller>(Lifestyle.Singleton);
            container.Register<TickerRequest>();
            container.Register<BalancesRequest>();
            container.Register<PlaceOrderRequest>();
            container.Register<TradesRequest>();
            container.Register<ITheRockRequestFactory>(() => new TheRockRequestFactory(container), Lifestyle.Singleton);
            container.Register(() => new HttpClient(), Lifestyle.Singleton);
            container.Register(() => new ThrottlingDelegatingHandler(new System.Threading.SemaphoreSlim(1), container.GetInstance<ApiConfig>()) { InnerHandler = new HttpClientHandler() },
                Lifestyle.Singleton);
            return container;
        }
    }
}
