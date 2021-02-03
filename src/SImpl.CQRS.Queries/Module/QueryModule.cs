using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SImpl.CQRS.Queries.Services;
using SImpl.Modules;

namespace SImpl.CQRS.Queries.Module
{
    public class QueryModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(QueryModule);

        public QueryModuleConfig Config { get; }
        
        public QueryModule(QueryModuleConfig config)
        {
            Config = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            if (Config.EnableInMemoryQueryDispatcher)
            {
                services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
                services.AddSingleton<IInMemoryQueryDispatcher, InMemoryQueryDispatcher>();
            }

            services.Scan(s =>
                s.FromAssemblies(Config.RegisteredAssemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            foreach (var queryHandlerType in Config.RegisteredQueryHandlers)
            {
                // TODO:
            }
        }
    }
}