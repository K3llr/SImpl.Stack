using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly Action<IApplicationBuilder> _configureDelegate;
        private Action<IServiceCollection> _servicesDelegate;

        public ApplicationBuilder(IDotNetStackHostBuilder stackHostBuilder, Action<IApplicationBuilder> configureDelegate, Action<IServiceCollection> servicesDelegate)
        {
            HostBuilder = stackHostBuilder;

            _configureDelegate = configureDelegate;
            _servicesDelegate = servicesDelegate;
        }

        public IDotNetStackHostBuilder HostBuilder { get; }

        public void Configure()
        {
            _configureDelegate?.Invoke(this);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _servicesDelegate?.Invoke(services);
        }
    }
}