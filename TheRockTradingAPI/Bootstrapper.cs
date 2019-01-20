using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;
using Newtonsoft.Json;
using TheRockTradingAPI.restUtils;
using TheRockTradingAPI.request;
using TheRockTradingAPI.contract;
using System.IO;

namespace TheRockTradingAPI
{
    
    internal class Bootstrapper
    {

        public static IServiceProvider Bootstrap()
        {
            var container = new Container();
            container.Register<ApiConfig>(Lifestyle.Singleton);
            container.Register<RestCaller>(Lifestyle.Singleton);
            container.Register<IRestCaller, RestCaller>(Lifestyle.Singleton);
            container.Register<TickerRequest>();
            container.Register<BalancesRequest>();
            container.Register<ITheRockRequestFactory>(() => new TheRockRequestFactory(container), Lifestyle.Singleton);
            return container;
        }
    }
}
