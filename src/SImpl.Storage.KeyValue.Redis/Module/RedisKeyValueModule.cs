using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.KeyValue.Redis.Services;
using SImpl.Storage.Redis.Module;

namespace SImpl.Storage.KeyValue.Redis.Module
{
    [DependsOn(typeof(RedisModule))]
    public class RedisKeyValueModule : IServicesCollectionConfigureModule
    {
        public RedisKeyValueConfig Config { get; }

        public RedisKeyValueModule(RedisKeyValueConfig config)
        {
            Config = config;
        }
        
        public string Name { get; } = nameof(RedisKeyValueModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Register model settings
            foreach (var modelSetting in Config.RegisteredModelStorageSettings)
            {
                services.AddSingleton(modelSetting.StorageSettings);
            }
            
            // Register open generic
            services.AddScoped(typeof(IRedisKeyValueStorage<,>), typeof(RedisKeyValueStorage<,>));
        }
    }
}