using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Collections.Concurrent;
using TheRockTradingAPI.contract;
using System.Reactive;
using System.Threading;

namespace TheRockTradingAPI.restUtils
{
    internal class ThrottledRestCaller : IRestCaller, IDisposable
    {
        private readonly RestCaller restCaller;
        private readonly BlockingCollection<QueuedRestCall> blockingQueue;
        private readonly ApiConfig apiConfig;
        private IObservable<long> timer;
        private IDisposable timerSubscriber;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public ThrottledRestCaller(RestCaller restCaller, ApiConfig config)
        {
            this.apiConfig = config;
            this.restCaller = restCaller;
            blockingQueue = new BlockingCollection<QueuedRestCall>(this.apiConfig.MaxEnqueuedCallsInThrottledCaller);
            this.timer = Observable.Interval(TimeSpan.FromMilliseconds(this.apiConfig.RestCallDelayMilliseconds));

            this.timerSubscriber = timer.Subscribe((time) => 
            {
                try
                {
                    Console.WriteLine($"starting take: {time}");
                    var restCall = this.blockingQueue.Take(cancellationTokenSource.Token);
                    Console.WriteLine("taken");

                    Console.WriteLine("starting invoke:");
                    IResponse response = null;
                    response = restCall.CallFunc?.Invoke();
                    //try
                    //{
                    //    response = restCall.CallFunc?.Invoke();
                    //}
                    //catch (Exception e)
                    //{
                    //    response = new response.EmptyResponse { Exception = e };
                    //}
                    //finally
                    //{
                    //    restCall.Response.Add(response);
                    //}
                    
                    Console.WriteLine($"{DateTime.Now.ToString("O")}: invoked");
                    restCall.Response.CompleteAdding();
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
        }

        public void Dispose()
        {
            Console.WriteLine($"{DateTime.Now.ToString("O")}: Cancellation Token firing");
            this.cancellationTokenSource.Cancel(true);
            Console.WriteLine($"{DateTime.Now.ToString("O")}: Cancellation Token fired");
            this.timerSubscriber.Dispose();
            this.restCaller.Dispose();
            this.blockingQueue.Dispose();
        }

        public T Get<T>(string uri) where T : IResponse
        {
            return this.GetAsync<T>(uri).Result;
        }

        public async Task<T> GetAsync<T>(string uri) where T : IResponse
        {
            return await Task.Run(() => 
            {
                var action = new QueuedRestCall
                {
                    CallFunc = () => 
                    {
                        return this.restCaller.Get<T>(uri);
                    }
                };

                Console.WriteLine($"{DateTime.Now.ToString("O")}: - start TryAdd");
                if(!this.blockingQueue.TryAdd(action, this.apiConfig.TimeOutMilliseconds, this.cancellationTokenSource.Token))
                {
                    throw new Exception("trying to add action: unable to add action to blockingQueue, or cancellationToken has been fired");
                }
                Console.WriteLine($"{DateTime.Now.ToString("O")}: - end TryAdd");

                Console.WriteLine($"{DateTime.Now.ToString("O")}: - start TryTake");
                IResponse ret = null;
                if(!action.Response.TryTake(out ret, this.apiConfig.TimeOutMilliseconds, this.cancellationTokenSource.Token))
                {
                    throw new Exception("trying to take response: unable to retrieve a response in time, or cancellationtoken has been fired");
                }
                Console.WriteLine($"{DateTime.Now.ToString("O")}: - end TryTake");

                if (ret.Exception != null)
                {
                    throw ret.Exception;
                };

                return (T)ret;
            });
        }

    }

    internal class QueuedRestCall
    {
        public Func<IResponse> CallFunc;
        public BlockingCollection<IResponse> Response = new BlockingCollection<IResponse>(1);
    }
}
