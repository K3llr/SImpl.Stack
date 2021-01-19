using System;
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

        public void Configure(Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            _logger.LogDebug("   ApplicationBuilder > Configure > started");
            _applicationBuilder.Configure(configureDelegate);
            _logger.LogDebug("   ApplicationBuilder > Configure > ended");
        }

        public IDotNetStackApplication Build()
        {
            _logger.LogDebug("   ApplicationBuilder > Building application > building");
            var application = new VerboseApplication(_applicationBuilder.Build(), _logger);
            _logger.LogDebug("   ApplicationBuilder > Building application > built");

            return application;
        }
    }
}