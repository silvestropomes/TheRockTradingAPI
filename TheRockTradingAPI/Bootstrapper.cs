using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TheRockTradingAPI.restUtils;
using TheRockTradingAPI.request;
using TheRockTradingAPI.contract;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TheRockTradingAPI
{
    
    internal class Bootstrapper
    {

        public static IServiceProvider Bootstrap()
        {
            var container = new ServiceCollection();
            container.AddSingleton<ApiConfig>();
            container.AddSingleton<IRestCaller, RestCaller>();
            container.AddSingleton<ITheRockRequestFactory>((_provider) => new TheRockRequestFactory(_provider));
            container.AddSingleton((_provider) => new HttpClient(_provider.GetService<ThrottlingDelegatingHandler>()));
            container.AddSingleton((_provider) => new ThrottlingDelegatingHandler(new System.Threading.SemaphoreSlim(1), _provider.GetService<ApiConfig>()) { InnerHandler = new HttpClientHandler() });

            container.AddTransient<TickerRequest>();
            container.AddTransient<BalancesRequest>();
            container.AddTransient<PlaceOrderRequest>();
            container.AddTransient<TradesRequest>();

            return container.BuildServiceProvider();
        }
    }
}
