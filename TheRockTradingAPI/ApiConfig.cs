using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockTradingAPI
{
    public class ApiConfig
    {
        public string BasePath => "https://api.therocktrading.com";
        public string ApiKey { get; set; }
        public string PrivateKey { get; set; }

        public bool AreKeysPresent => !(string.IsNullOrEmpty(this.ApiKey) || string.IsNullOrEmpty(this.PrivateKey));

        public int MaxEnqueuedCallsInThrottledCaller => 20;

        public double RestCallDelayMilliseconds => 100;

        public int TimeOutMilliseconds => 10000;
    }
}
