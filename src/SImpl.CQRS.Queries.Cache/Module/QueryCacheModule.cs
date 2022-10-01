using Microsoft.Extensions.DependencyInjection;
using SImpl.Cache;
using SImpl.Cache.Module;
using SImpl.CQRS.Queries.Cache.Decorators;
using SImpl.CQRS.Queries.Cache.Services;
using SImpl.CQRS.Queries.Module;
using SImpl.Host.Builders;
using SImpl.Modules;

namespace SImpl.CQRS.Queries.Cache.Module
{
    [DependsOn(typeof(CacheModule), typeof(QueryModule))]
    public class QueryCacheModule : IHostBuilderConfigureModule, IServicesCollectionConfigureModule, ICacheExtensionModule
    {
        public QueryCacheModuleConfig Config { get; }

        public QueryCacheModule(QueryCacheModuleConfig config)
        {
            Config = config;
        }
        
        public string Name => nameof(QueryCacheModule);

        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.UseCache();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config);
            
            services.AddSingleton<IQueryCacheKeyService, QueryCacheKeyService>();
            services.AddSingleton<IQueryCacheManager, QueryCacheManager>();
            services.AddSingleton<IQueryCacheInvalidator, QueryCacheInvalidator>();

            services.Decorate(typeof(IQueryHandler<,>), typeof(QueryHandlerCacheDecorator<,>));
        }
    }
}