using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.HostBuilders
{
    public interface IDotNetStackHostBuilder : IHostBuilder
    {
        IDotNetStackRuntime Runtime { get; }

        void Use<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;
        
        TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;

        void Configure(Action<IDotNetStackHostBuilder> configureDelegate);
    }
}