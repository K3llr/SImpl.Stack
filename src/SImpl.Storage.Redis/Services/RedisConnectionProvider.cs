using System;
using SImpl.Exceptions;
using SImpl.Storage.Redis.Module;
using StackExchange.Redis;

namespace SImpl.Storage.Redis.Services
{
    public class RedisConnectionProvider : IRedisConnectionProvider
    {
        private readonly RedisConnectionConfig _redisConnectionConfig;
        
        private readonly object _connectionInitLock = new object();
        private ConnectionMultiplexer _connection;
        private DateTime _connectionDate;

        public RedisConnectionProvider(RedisConnectionConfig redisConnectionConfig)
        {
            _redisConnectionConfig = redisConnectionConfig;
        }

        public ConnectionMultiplexer Connection
        {
            get
            {
                var now = DateTime.UtcNow;
                if (_connection is null || !_connection.IsConnected || HasTimeToLiveExpired(now))
                {
                    lock (_connectionInitLock)
                    {
                        if (_connection is null || !_connection.IsConnected || HasTimeToLiveExpired(now))
                        {
                            _connection?.Close();

                            var connectionString = _redisConnectionConfig.ConnectionString;
                            if (connectionString is null || string.IsNullOrEmpty(connectionString))
                            {
                               throw new InvalidConfigurationException($"ConnectionString was null or empty.");
                            }

                            var configurationOptions = ConfigurationOptions.Parse(connectionString);
                            if (_redisConnectionConfig.OverrideSslEnabled)
                            {
                                // Enable SSL if override is enabled
                                configurationOptions.Ssl = true;
                            }

                            if (_redisConnectionConfig.IgnoreSslErrors)
                            {
                                // Register callback to ignore certificate validation
                                configurationOptions.CertificateValidation += (sender, certificate, chain, errors) => true;
                            }

                            // Should default to 5 seconds sync timeout because Redis will timeout if the value is too large
                            if (!connectionString.ToLower().Contains("synctimeout") && configurationOptions.SyncTimeout < 5000)
                            {
                                configurationOptions.SyncTimeout = 5000; 
                            }

                            _connection = ConnectionMultiplexer.Connect(configurationOptions);
                            _connectionDate = now;
                        }
                    }
                }

                return _connection;
            }
        }

        private bool HasTimeToLiveExpired(DateTime now)
        {
            return (now - _connectionDate).TotalSeconds > _redisConnectionConfig.ConnectionTimeToLive;
        }
    }
}