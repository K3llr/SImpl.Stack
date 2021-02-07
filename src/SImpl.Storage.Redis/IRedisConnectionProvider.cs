using StackExchange.Redis;

namespace SImpl.Storage.Redis
{
    public interface IRedisConnectionProvider
    {
        ConnectionMultiplexer Connection { get; }
    }
}