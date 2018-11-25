using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockTradingAPI.contract
{
    public interface IPostRequest : IRequest
    {
        string GetPostContent();
    }
}
