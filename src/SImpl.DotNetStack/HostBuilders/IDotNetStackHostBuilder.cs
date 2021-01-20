using System;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.HostBuilders
{
    public interface IDotNetStackHostBuilder : IHostBuilder
    {
        void Use<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;
        
        TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;

        void Configure(IDotNetStackHostBuilder hostBuilder, Action<IDotNetStackHostBuilder> configureDelegate);
    }
}