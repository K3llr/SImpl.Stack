using System;
using SImpl.Common;
using SImpl.Storage.Redis.Services;

namespace SImpl.Storage.Redis.Module
{
    public class RedisConfig
    {
        public RedisConnectionConfig RedisConnectionConfig { get; private set; }

        public RedisConfig SetConnection(Action<RedisConnectionConfig> configure)
        {
            RedisConnectionConfig ??= new RedisConnectionConfig();
            configure.Invoke(RedisConnectionConfig);
            return this;
        }

        public TypeOf<IRedisSerializationService> SerializationServiceType { get; private set; }
            = TypeOf.New<IRedisSerializationService, JsonRedisSerializationService>();

        public RedisConfig OverrideSerializationService<TSerializationServiceType>()
        
        where TSerializationServiceType : IRedisSerializationService
        {
            var type = TypeOf.New<IRedisSerializationService, TSerializationServiceType>();
            OverrideSerializationService(type);
            return this;
        }
        
        public RedisConfig OverrideSerializationService(TypeOf<IRedisSerializationService> type)
        {
            SerializationServiceType = type;
            return this;
        }
    }
}