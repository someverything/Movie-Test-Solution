﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using MovieTestSolution.Core.Utilities.IoC;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheManager(IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = ServiceTool.ServiceProvider.GetService<IDistributedCache>();
            _connectionMultiplexer = connectionMultiplexer;
        }

        public void Add(string key, object value, int duration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(duration)
            };
            var jsonValue = JsonConvert.SerializeObject(value);
            _distributedCache.SetString(key, jsonValue, options);
        }

        public T Get<T>(string key)
        {
            var jsonValue = _distributedCache.GetString(key);
            if (jsonValue == null)
                return default;
            var a = JsonConvert.DeserializeObject<T>(jsonValue);
            return a;
        }

        public object Get(string key)
        {
            var jsonValue = _distributedCache.GetString(key);
            if (jsonValue == null)
                return null;

            return JsonConvert.DeserializeObject<object>(jsonValue);
        }

        public bool IsAdd(string key)
        {
            var jsonValue = _distributedCache.GetString(key);
            return jsonValue != null;
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var server = GetServer();
            var keysToRemove = new List<RedisKey>();

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (var key in server.Keys())
            {
                if (regex.IsMatch(key))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                _distributedCache.Remove(key);
            }
        }

        private IServer GetServer()
        {
            var endpoints = _connectionMultiplexer.GetEndPoints();
            return _connectionMultiplexer.GetServer(endpoints.First());
        }
    }
}
