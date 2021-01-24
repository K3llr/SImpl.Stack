using System;
using Microsoft.Extensions.DependencyInjection;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly Action<IApplicationBuilder> _configureDelegate;
        private readonly Action<IServiceCollection> _servicesDelegate;

        public ApplicationBuilder(Action<IApplicationBuilder> configureDelegate, Action<IServiceCollection> servicesDelegate)
        {
            _configureDelegate = configureDelegate;
            _servicesDelegate = servicesDelegate;
        }

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