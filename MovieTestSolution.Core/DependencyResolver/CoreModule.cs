using Microsoft.Extensions.DependencyInjection;
using MovieTestSolution.Core.CrossCuttingConcerns.Caching.Redis;
using MovieTestSolution.Core.CrossCuttingConcerns.Caching;
using MovieTestSolution.Core.Utilities.IoC;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.DependencyResolver
{
    public class CoreModule : ICoreModules
    {
        public void Load(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMemoryCache();

            #region Redis
            serviceDescriptors.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = "localhost:1234";
            });
            serviceDescriptors.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:1234"));
            #endregion

            serviceDescriptors.AddTransient<ICacheManager, RedisCacheManager>();
        }
    }
}
