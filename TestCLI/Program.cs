using System;
using System.Collections.Generic;
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
            var client = new TheRockApiClient();

            var requestFactory = client.TheRockRequestFactory;
            try
            {
                //var order = client.Post<OrderResponse>(requestFactory.GetPlaceBuyOrderRequest("LTCEUR", 0.01m, 1m));
                var r = requestFactory.GetUserTradesRequest("LTCEUR", null, null, null, new DateTime(2019, 2, 10), new DateTime(2019, 2, 20));
                var ltcTrades =  client.Get<TradesResponse>(r);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var balances = client.Get<BalancesResponse>(requestFactory.GetBalancesRequest());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var responseList = new List<Task<TickerResponse>>();

            Console.WriteLine(DateTime.Now.ToString("o"));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR")));
            responseList.Add(client.GetAsync<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR")));
            Task.WaitAll(responseList.ToArray());
            Console.WriteLine(DateTime.Now.ToString("o"));



            Console.WriteLine(DateTime.Now.ToString("o"));
            var tickerBTCEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR"));
            var tickerETHEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR"));
            var tickerLTCEUR = client.Get<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR"));

            var tickerBTCEUR2 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR"));
            var tickerETHEUR2 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR"));
            var tickerLTCEUR2 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR"));

            var tickerBTCEUR3 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR"));
            var tickerETHEUR3 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR"));
            var tickerLTCEUR3 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR"));

            var tickerBTCEUR4 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("BTCEUR"));
            var tickerETHEUR4 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("ETHEUR"));
            var tickerLTCEUR4 = client.Get<TickerResponse>(requestFactory.GetTickerRequest("LTCEUR"));
            Console.WriteLine(DateTime.Now.ToString("o"));
        }
    }
}
