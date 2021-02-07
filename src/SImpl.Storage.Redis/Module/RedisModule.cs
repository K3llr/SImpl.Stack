using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

namespace SImpl.Storage.Redis.Module
{
    public class RedisModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(RedisModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}