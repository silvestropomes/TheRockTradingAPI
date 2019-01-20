using System;
using System.Threading.Tasks;
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
            using (var client = new TheRockApiClient())
            {
                var requestFactory = client.TheRockRequestFactory;
                try
                {
                    var balances = client.Get<BalancesResponse>(requestFactory.GetBalancesRequest());
                }
                catch (Exception e)
                {
                    Console.WriteLine($"program: {e.Message}");
                }

                var a1 = client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR"));
                var a2 = client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR"));
                var a3 = client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR"));
                Console.WriteLine($"before waitAll: {DateTime.Now.ToLongTimeString()}");
                Task.WaitAll(a1, a2, a3);
                Console.WriteLine($"after waitAll: {DateTime.Now.ToLongTimeString()}");

                //System.Threading.Thread.Sleep(10000);

                var r1 = a1.Result;
                var r2 = a2.Result;
                var r3 = a3.Result;

                Console.WriteLine($"ticker: {DateTime.Now.ToLongTimeString()}");
                var tickerBTCEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR"));

                Console.WriteLine($"ticker: {DateTime.Now.ToLongTimeString()}");
                var tickerETHEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR"));

                Console.WriteLine($"ticker: {DateTime.Now.ToLongTimeString()}");
                var tickerLTCEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR"));
            }
        }
    }
}
