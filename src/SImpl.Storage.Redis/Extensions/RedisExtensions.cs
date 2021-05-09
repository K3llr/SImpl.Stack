using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace SImpl.Storage.Redis.Extensions
{
    public static class RedisExtensions
    {
        public static T GetValueOrDefault<T>(this IDictionary<RedisValue, RedisValue> properties, string key, Func<RedisValue, T> factory)
        {
            return properties.ContainsKey(key)
                ? properties[key].GetValueOrDefault(factory)
                : default(T);
        }
        
        public static T GetValueOrDefault<T>(this RedisValue entry, Func<RedisValue, T> factory)
        {
            return !entry.HasValue ? default(T) : factory(entry);
        }
    }
}