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
using Microsoft.Extensions.Configuration;
using MovieTestSolution.Core.CrossCuttingConcerns.Caching.Microsoft;

namespace MovieTestSolution.Core.DependencyResolver
{
    public class CoreModule : ICoreModules
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public CoreModule(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Load(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMemoryCache();

            bool useRedis = _configuration.GetValue<bool>("UseRedis");

            if (useRedis)
            {
                #region Redis Configuration
                var redisConfig = new ConfigurationOptions
                {
                    EndPoints = { "localhost:1234" }, // Use your Redis port
                    AbortOnConnectFail = false
                };

                serviceDescriptors.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = "localhost:1234,abortConnect=false";
                });

                serviceDescriptors.AddSingleton<IConnectionMultiplexer>(sp =>
                    ConnectionMultiplexer.Connect(redisConfig));

                serviceDescriptors.AddSingleton<ICacheManager, RedisCacheManager>();
                #endregion
            }
            else
            {
                // Use MemoryCacheManager
                serviceDescriptors.AddSingleton<ICacheManager, MemoryCacheManager>();
            }
        }
    }
}
