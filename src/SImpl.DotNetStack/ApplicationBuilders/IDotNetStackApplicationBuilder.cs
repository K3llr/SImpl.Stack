using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public interface IDotNetStackApplicationBuilder
    {
        void Use<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;
        
        TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;
        
        IDotNetStackApplicationBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);
        
        IDotNetStackApplicationBuilder ConfigureServices(IServiceCollection serviceCollection);
        
        IDotNetStackApplicationBuilder ConfigureApplication();
        
        IDotNetStackApplication Build();
        
        void Configure(Action<IDotNetStackApplicationBuilder> configureDelegate);
    }
}