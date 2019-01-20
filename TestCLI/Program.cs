using System;
using TheRockTradingAPI;
using TheRockTradingAPI.response;

namespace TestCLI
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting program");
            var p = new Program();
            p.Run(args);
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }

        public void Run(string[] args)
        {
            var client = new TheRockApiClient();
            var requestFactory = client.TheRockRequestFactory;
            try
            {
                var balances = client.Get<BalancesResponse>(requestFactory.GetBalancesRequest());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var tickerBTCEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR"));
            var tickerETHEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR"));
            var tickerLTCEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR"));

        }
    }
}
