using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockTradingAPI.contract
{
    public interface IResponse
    {
        Exception Exception { get; set; }
    }
}
