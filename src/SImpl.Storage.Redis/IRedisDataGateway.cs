using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace SImpl.Storage.Redis
{
    public interface IRedisDataGateway
    {
        IDatabase GetDatabase(int database);
        void ClearDatabase(int database);
        T Get<T>(int database, string key);
        Tuple<T1, T2> Get<T1, T2>(int database, string key1, string key2);
        Tuple<T1, T2, T3> Get<T1, T2, T3>(int database, string key1, string key2, string key3);
        Tuple<T1, T2, T3, T4> Get<T1, T2, T3, T4>(int database, string key1, string key2, string key3, string key4);
        IReadOnlyList<T> GetMultiple<T>(int database, params string[] keys);

        void Set<T>(int database, string key, T value, TimeSpan? expiry = default(TimeSpan?));
        void Set<T1, T2>(int database, string key1, T1 value1, string key2, T2 value2, TimeSpan? expiry = default(TimeSpan?));
        void Set<T1, T2, T3>(int database, string key1, T1 value1, string key2, T2 value2, string key3, T3 value3, TimeSpan? expiry = default(TimeSpan?));
        void Set<T1, T2, T3, T4>(int database, string key1, T1 value1, string key2, T2 value2, string key3, T3 value3, string key4, T3 value4, TimeSpan? expiry = default(TimeSpan?));

        long Increase(int database, string key);
        long IncreaseBy(int database, string key, long amount);
        long Decrease(int database, string key);
        long DecreaseBy(int database, string key, long amount);

        bool Exists(int database, string key);
        void Remove(int database, string key);
        void Remove(int database, string[] keys);

        T HashGet<T>(int database, string key, string field);

        void HashSet<T>(int database, string key, string field, T value);

        bool HashExists(int database, string key, string field);
        void HashRemove(int database, string key, string field);
        void HashRemove(int database, string key, string[] fields);
    }
}