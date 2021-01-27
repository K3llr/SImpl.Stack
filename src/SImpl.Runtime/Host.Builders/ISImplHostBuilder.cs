using System;
using Microsoft.Extensions.Hosting;
using SImpl.Modules;

namespace SImpl.Runtime.Host.Builders
{
    public interface ISImplHostBuilder : IHostBuilder
    {
        ISImplHostBuilder Use<TModule>(Func<TModule> factory)
            where TModule : ISImplModule;
        
        TModule GetConfiguredModule<TModule>()
            where TModule : ISImplModule;

        TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : ISImplModule;

        void Configure(ISImplHostBuilder hostBuilder, Action<ISImplHostBuilder> configureDelegate);
    }
}