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

namespace TheRockTradingAPI.restUtils
{
    internal class ThrottledRestCaller : IRestCaller, IDisposable
    {
        private readonly RestCaller restCaller;
        private readonly BlockingCollection<QueuedRestCall> blockingQueue;
        private readonly int maxEnqueuedCalls = 3;
        private IObservable<long> timer = Observable.Interval(TimeSpan.FromSeconds(1));
        private IDisposable timerSubscriber;

        public ThrottledRestCaller(RestCaller restCaller)
        {
            this.restCaller = restCaller;
            blockingQueue = new BlockingCollection<QueuedRestCall>(maxEnqueuedCalls);
            this.timerSubscriber = timer.Subscribe((time) => 
            {
                var restCall = this.blockingQueue.Take(); // cancellation token

                restCall.Response.Add(restCall.CallFunc?.Invoke());
                restCall.Response.CompleteAdding();
                Console.WriteLine(time);
            });
        }

        public void Dispose()
        {
            this.restCaller.Dispose();
            this.blockingQueue.Dispose();
            this.timerSubscriber.Dispose();
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
                this.blockingQueue.Add(action); // add cancellation token (for timeout)
                return (T)action.Response.Take(); // add cancellation token (for timeout)
            });
        }

    }

    internal class QueuedRestCall
    {
        public Func<IResponse> CallFunc;
        public BlockingCollection<IResponse> Response = new BlockingCollection<IResponse>(1);
    }
}
