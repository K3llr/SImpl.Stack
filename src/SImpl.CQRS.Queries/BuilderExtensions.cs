using System;
using SImpl.CQRS.Queries.Module;
using SImpl.Host.Builders;

namespace SImpl.CQRS.Queries
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseCqrsInMemoryQueryDispatcher(this ISImplHostBuilder host)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new QueryModule(new QueryModuleConfig()));
            module.Config.EnableInMemoryQueryDispatcher = true;
            return host;
        }

        public static ISImplHostBuilder UseCqrsQueries(this ISImplHostBuilder host, Action<QueryModuleConfig> configureDelegate)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new QueryModule(new QueryModuleConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}