using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

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
            
        }
    }
}