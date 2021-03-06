using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.Redis.Services;

namespace SImpl.Storage.Redis.Module
{
    public class RedisModule : IServicesCollectionConfigureModule
    {
        public RedisConfig Config { get; }

        public RedisModule(RedisConfig config)
        {
            Config = config;
        }
        
        public string Name { get; } = nameof(RedisModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(RedisConnectionConfig), Config.RedisConnectionConfig);
            services.AddSingleton<IRedisConnectionProvider, RedisConnectionProvider>();
            services.AddSingleton(typeof(IRedisSerializationService), Config.SerializationServiceType.ImplType);
            services.AddSingleton<IRedisDataGateway, RedisDataGateway>();
        }
    }
}