using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseApplicationBuilder : IDotNetStackApplicationBuilder
    {
        private readonly IDotNetStackApplicationBuilder _applicationBuilder;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseApplicationBuilder(IDotNetStackApplicationBuilder applicationBuilder, ILogger<VerboseHost> logger)
        {
            _applicationBuilder = applicationBuilder;
            _logger = logger;
        }
        
        public void Use<TModule>(Func<TModule> factory) 
            where TModule : IApplicationModule
        {
            _applicationBuilder.Use(factory);
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory) 
            where TModule : IApplicationModule
        {
            return _applicationBuilder.AttachNewOrGetConfiguredModule(factory);
        }

        public IDotNetStackApplicationBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
        {
            return _applicationBuilder.ConfigureServices(configureDelegate);
        }

        public IDotNetStackApplicationBuilder ConfigureServices(IServiceCollection serviceCollection)
        {
            return _applicationBuilder.ConfigureServices(serviceCollection);
        }

        public IDotNetStackApplicationBuilder ConfigureApplication()
        {
            return _applicationBuilder.ConfigureApplication();
        }

        public IDotNetStackApplication Build()
        {
            _logger.LogDebug("   ApplicationBuilder > Application building");
            var application = new VerboseApplication(_applicationBuilder.Build(), _logger);
            _logger.LogDebug("   ApplicationBuilder > Application built");

            return application;
        }

        public void Configure(Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            _logger.LogDebug("   ApplicationBuilder > Module configuration started");
            _applicationBuilder.Configure(configureDelegate);
            _logger.LogDebug("   ApplicationBuilder > Module configuration ended");

        }
    }
}