using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

namespace SImpl.Storage.KeyValue.Redis.Module
{
    public class RedisKeyValueModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(RedisKeyValueModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}