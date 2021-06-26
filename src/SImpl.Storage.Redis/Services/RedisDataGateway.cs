using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace SImpl.Storage.Redis.Services
{
    public class RedisDataGateway : IRedisDataGateway
    {
        private readonly IRedisConnectionProvider _connectionProvider;
        private readonly IRedisSerializationService _serializationService;

        public RedisDataGateway(
            IRedisConnectionProvider connectionProvider,
            IRedisSerializationService serializationService)
        {
            _connectionProvider = connectionProvider;
            _serializationService = serializationService;
        }

        public virtual IDatabase GetDatabase(int database)
        {
            return _connectionProvider.Connection.GetDatabase(database);
        }

        public void ClearDatabase(int database)
        {
            var endpoints = _connectionProvider.Connection.GetEndPoints();
            var server = _connectionProvider.Connection.GetServer(endpoints.First());
            server.FlushDatabase(database);
        }

        protected virtual RedisValue SerializeObject<T>(T entry)
        {
            return _serializationService.SerializeObject(entry);
        }

        protected virtual T DeserializeObject<T>(RedisValue entry)
        {
            return _serializationService.DeserializeObject<T>(entry);
        }
        
        public virtual T Get<T>(int database, string key)
        {
            try
            {
                // Get db
                var db = GetDatabase(database);

                // Get value
                var redisValue = db.StringGet(key, CommandFlags.PreferReplica);

                return GetValueOrDefault<T>(redisValue);
            }
            catch (Exception ex)
            {
                var test = ex;
                return default(T);
            }
        }

        public virtual Tuple<T1, T2> Get<T1, T2>(int database, string key1, string key2)
        {
            try
            {
                // Get db
                var db = GetDatabase(database);

                var redisKeys = ConvertToRedisKeys(new[]
                {
                    key1, key2
                });

                // Get value
                var redisValues = db.StringGet(redisKeys, CommandFlags.PreferReplica);

                var item1 = GetValueOrDefault<T1>(redisValues[0]);
                var item2 = GetValueOrDefault<T2>(redisValues[1]);

                return new Tuple<T1, T2>(item1, item2);
            }
            catch
            {
                return default(Tuple<T1, T2>);
            }
        }

        public virtual Tuple<T1, T2, T3> Get<T1, T2, T3>(int database, string key1, string key2, string key3)
        {
            try
            {
                // Get db
                var db = GetDatabase(database);

                var redisKeys = ConvertToRedisKeys(new[]
                {
                    key1, key2, key3
                });

                // Get value
                var redisValues = db.StringGet(redisKeys, CommandFlags.PreferReplica);

                var item1 = GetValueOrDefault<T1>(redisValues[0]);
                var item2 = GetValueOrDefault<T2>(redisValues[1]);
                var item3 = GetValueOrDefault<T3>(redisValues[2]);

                return new Tuple<T1, T2, T3>(item1, item2, item3);
            }
            catch
            {
                return default(Tuple<T1, T2, T3>);
            }
        }

        public virtual Tuple<T1, T2, T3, T4> Get<T1, T2, T3, T4>(int database, string key1, string key2, string key3, string key4)
        {
            try
            {
                // Get db
                var db = GetDatabase(database);

                var redisKeys = ConvertToRedisKeys(new[]
                {
                    key1, key2, key3, key4
                });

                // Get value
                var redisValues = db.StringGet(redisKeys, CommandFlags.PreferReplica);

                var item1 = GetValueOrDefault<T1>(redisValues[0]);
                var item2 = GetValueOrDefault<T2>(redisValues[1]);
                var item3 = GetValueOrDefault<T3>(redisValues[2]);
                var item4 = GetValueOrDefault<T4>(redisValues[3]);

                return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
            }
            catch
            {
                return default(Tuple<T1, T2, T3, T4>);
            }
        }

        public virtual IReadOnlyList<T> GetMultiple<T>(int database, params string[] keys)
        {
            try
            {
                // Get db
                var db = GetDatabase(database);

                var redisKeys = ConvertToRedisKeys(keys);

                // Get value
                var redisValues = db.StringGet(redisKeys, CommandFlags.PreferReplica);

                var values = new T[keys.Length];

                for (int i = 0; i < keys.Length; i++)
                {
                    values[i] = GetValueOrDefault<T>(redisValues[i]);
                }

                return values;
            }
            catch
            {
                return new List<T>();
            }
        }

        public virtual void Set<T>(int database, string key, T value, TimeSpan? expiry = default(TimeSpan?))
        {
            // Get db
            var db = GetDatabase(database);

            var serializedValue = SerializeObject(value);

            // Set to redis
            db.StringSet(key, serializedValue, expiry, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);
        }

        public virtual void Set<T1, T2>(int database, string key1, T1 value1, string key2, T2 value2, TimeSpan? expiry = default(TimeSpan?))
        {
            // Get db
            var db = GetDatabase(database);

            var redisKeyValues = new[]
            {
                new KeyValuePair<RedisKey, RedisValue>(key1, SerializeObject(value1)),
                new KeyValuePair<RedisKey, RedisValue>(key2, SerializeObject(value2))
            };

            // Set to redis
            db.StringSet(redisKeyValues, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);

            // Set expiry
            if (expiry.HasValue)
            {
                foreach (var redisKeyValue in redisKeyValues)
                {
                    db.KeyExpire(redisKeyValue.Key, expiry, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);
                }
            }
        }

        public virtual void Set<T1, T2, T3>(int database, string key1, T1 value1, string key2, T2 value2, string key3, T3 value3, TimeSpan? expiry = default(TimeSpan?))
        {
            // Get db
            var db = GetDatabase(database);

            var redisKeyValues = new[]
            {
                new KeyValuePair<RedisKey, RedisValue>(key1, SerializeObject(value1)),
                new KeyValuePair<RedisKey, RedisValue>(key2, SerializeObject(value2)),
                new KeyValuePair<RedisKey, RedisValue>(key3, SerializeObject(value3))
            };

            // Set to redis
            db.StringSet(redisKeyValues, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);

            // Set expiry
            if (expiry.HasValue)
            {
                foreach (var redisKeyValue in redisKeyValues)
                {
                    db.KeyExpire(redisKeyValue.Key, expiry, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);
                }
            }
        }

        public virtual void Set<T1, T2, T3, T4>(int database, string key1, T1 value1, string key2, T2 value2, string key3, T3 value3, string key4, T3 value4, TimeSpan? expiry = default(TimeSpan?))
        {
            // Get db
            var db = GetDatabase(database);

            var redisKeyValues = new[]
            {
                new KeyValuePair<RedisKey, RedisValue>(key1, SerializeObject(value1)),
                new KeyValuePair<RedisKey, RedisValue>(key2, SerializeObject(value2)),
                new KeyValuePair<RedisKey, RedisValue>(key3, SerializeObject(value3)),
                new KeyValuePair<RedisKey, RedisValue>(key4, SerializeObject(value4)),
            };

            // Set to redis
            db.StringSet(redisKeyValues, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);

            // Set expiry
            if (expiry.HasValue)
            {
                foreach (var redisKeyValue in redisKeyValues)
                {
                    db.KeyExpire(redisKeyValue.Key, expiry, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);
                }
            }
        }

        public long Increase(int database, string key)
        {
            return IncreaseBy(database, key, 1);
        }

        public long IncreaseBy(int database, string key, long amount)
        {
            // Get db
            var db = GetDatabase(database);

            return db.StringIncrement(key, amount, flags: CommandFlags.DemandMaster);
        }

        public long Decrease(int database, string key)
        {
            return DecreaseBy(database, key, 1);
        }

        public long DecreaseBy(int database, string key, long amount)
        {
            // Get db
            var db = GetDatabase(database);

            return db.StringDecrement(key, amount, flags: CommandFlags.DemandMaster);
        }

        public virtual bool Exists(int database, string key)
        {
            // Get db
            var db = GetDatabase(database);

            // Check existance
            return db.KeyExists(key, CommandFlags.PreferReplica);
        }

        public virtual void Remove(int database, string key)
        {
            // Get db
            var db = GetDatabase(database);

            // Remove
            db.KeyDelete(key, CommandFlags.DemandMaster);
        }

        public virtual void Remove(int database, string[] keys)
        {
            // Get db
            var db = GetDatabase(database);

            var redisKeys = ConvertToRedisKeys(keys);

            // Remove
            db.KeyDelete(redisKeys, CommandFlags.DemandMaster);
        }

        public T HashGet<T>(int database, string key, string field)
        {
            try
            {
                // Get db
                var db = GetDatabase(database);

                // Get value
                var redisValue = db.StringGet(key, CommandFlags.PreferReplica);

                return GetValueOrDefault<T>(redisValue);
            }
            catch (Exception ex)
            {
                var test = ex;
                return default(T);
            }
        }

        public void HashSet<T>(int database, string key, string field, T value)
        {
            // Get db
            var db = GetDatabase(database);

            var serializedValue = SerializeObject(value);

            // Set to redis
            db.HashSet(key, field, serializedValue, flags: CommandFlags.DemandMaster | CommandFlags.FireAndForget);
        }

        public bool HashExists(int database, string key, string field)
        {
            // Get db
            var db = GetDatabase(database);

            // Check existance
            return db.HashExists(key, field, CommandFlags.PreferReplica);
        }

        public void HashRemove(int database, string key, string field)
        {
            // Get db
            var db = GetDatabase(database);

            // Remove
            db.HashDelete(key, field, CommandFlags.DemandMaster);
        }

        public void HashRemove(int database, string key, string[] fields)
        {
            // Get db
            var db = GetDatabase(database);

            var redisFields = ConvertToRedisValues(fields);

            // Remove
            db.HashDelete(key, redisFields, CommandFlags.DemandMaster);
        }

        private RedisKey[] ConvertToRedisKeys(string[] keys)
        {
            return keys.Select(key => (RedisKey)key).ToArray();
        }

        private RedisValue[] ConvertToRedisValues(string[] values)
        {
            return values.Select(value => (RedisValue)value).ToArray();
        }

        private T GetValueOrDefault<T>(RedisValue entry)
        {
            return !entry.HasValue ? default(T) : DeserializeObject<T>(entry);
        }
    }
}