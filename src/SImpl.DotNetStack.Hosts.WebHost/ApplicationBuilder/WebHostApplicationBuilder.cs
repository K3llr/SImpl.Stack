using System;
using Microsoft.AspNetCore.Builder;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.Application;
using SImpl.DotNetStack.Hosts.WebHost.Modules;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.ApplicationBuilders;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder
{
    public class WebHostApplicationBuilder : DotNetStackApplicationBuilder, IWebHostApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly IApplicationBootManager _bootManager;

        public WebHostApplicationBuilder(IModuleManager moduleManager, IApplicationBootManager bootManager) :
            base(moduleManager)
        {
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }
        
        public void UseWebHostStackAppModule<TModule>(Func<TModule> factory) 
            where TModule : IWebHostApplicationModule
        {
            _moduleManager.AttachModule(factory.Invoke());
        }

        public TModule AttachNewWebHostStackAppModuleOrGetConfigured<TModule>(Func<TModule> factory) 
            where TModule : IWebHostApplicationModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public void Configure(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(this);
            
            _bootManager.Configure<IApplicationModule, IDotNetStackApplicationBuilder>(this);
            _bootManager.Configure<IWebHostApplicationModule, IWebHostApplicationBuilder>(this);
        }

        public override IDotNetStackApplication Build()
        {
            return new WebHostApplcation(_bootManager);
        }
    }
}