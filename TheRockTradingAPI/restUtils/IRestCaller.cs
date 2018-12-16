using System;
using System.Threading.Tasks;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.restUtils
{
    internal interface IRestCaller
    {
        T Get<T>(string uri) where T : IResponse;
        Task<T> GetAsync<T>(string uri) where T : IResponse;
    }
}