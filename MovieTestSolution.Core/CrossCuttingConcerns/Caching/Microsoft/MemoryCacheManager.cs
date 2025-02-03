using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {

        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key,value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            dynamic cacheEntriesCollection = null;
            var cacheEntiresFailedCollectionDefinition = typeof(MemoryCache).GetField("_coherentState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cachEntriesPropertyCollcetionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (cacheEntiresFailedCollectionDefinition != null )
            {
                var coherentStateValueCollection = cacheEntiresFailedCollectionDefinition.GetValue(_memoryCache);
                var entriesCollectionValuesCollection = coherentStateValueCollection?.GetType()
                    .GetProperty(
                        "EntriesCollection",
                         System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                    );

                cacheEntriesCollection = entriesCollectionValuesCollection.GetValue(coherentStateValueCollection);
            }

            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach ( var cachItem in cacheEntriesCollection )
            {
                var cacheItemValue = cachItem.GetType() .GetProperty("Value").GetValue(cachItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(x => regex.IsMatch(x.Key.ToString())).Select(x => x.Key).ToList();

            foreach(var key in keysToRemove )
            {
                _memoryCache.Remove(key);
            }

        }
    }
}
