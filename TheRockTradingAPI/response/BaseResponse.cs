using System;
using System.Collections.Generic;
using System.Text;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.response
{
    public abstract class BaseResponse : IResponse
    {
        public Exception Exception { get; set; }
    }
}
