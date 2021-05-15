using System;

namespace SImpl.Storage.Redis.Module
{
    public class RedisConfig
    {
        private RedisConnectionConfig RedisConnectionConfig { get; set; }

        public RedisConfig SetConnection(Action<RedisConnectionConfig> configure)
        {
            if (RedisConnectionConfig == null)
            {
                RedisConnectionConfig = new RedisConnectionConfig();
            }
            configure.Invoke(RedisConnectionConfig);
            return this;
        }
    }
}