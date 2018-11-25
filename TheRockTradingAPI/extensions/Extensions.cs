using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockTradingAPI.extensions
{
    public static class Extensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T) serviceProvider.GetService(typeof(T));
        }
    }
}
