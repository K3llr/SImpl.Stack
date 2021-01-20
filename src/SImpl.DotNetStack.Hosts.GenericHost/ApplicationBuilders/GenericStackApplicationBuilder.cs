using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
{
    public class GenericStackApplicationBuilder : IGenericStackApplicationBuilder
    {
        private readonly IDotNetStackApplicationBuilder _stackApplicationBuilder;
        private readonly Action<IGenericStackApplicationBuilder> _configureDelegate;

        private Action<IServiceCollection> _serviceDelegate;

        public GenericStackApplicationBuilder(IDotNetStackApplicationBuilder applicationBuilder, Action<IGenericStackApplicationBuilder> configureDelegate, Action<IServiceCollection> serviceDelegate)
        {
            _stackApplicationBuilder = applicationBuilder;
            _configureDelegate = configureDelegate;
            _serviceDelegate = serviceDelegate;
        }

        public IDotNetStackApplication Build()
        {
            return _stackApplicationBuilder.Build();
        }

        public void Use<TModule>(Func<TModule> factory) 
            where TModule : IApplicationModule
        {
            _stackApplicationBuilder.Use(factory);
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory) 
            where TModule : IApplicationModule
        {
            return _stackApplicationBuilder.AttachNewOrGetConfiguredModule(factory);
        }

        public void Configure(Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            _stackApplicationBuilder.Configure(configureDelegate);
        }
        
        public void Configure()
        {
            _configureDelegate?.Invoke(this);
            Configure(null); // TODO:
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _serviceDelegate?.Invoke(services);
        }
    }
}