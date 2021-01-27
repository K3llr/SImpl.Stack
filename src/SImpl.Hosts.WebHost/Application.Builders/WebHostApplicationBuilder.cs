using System;
using SImpl.Application;
using SImpl.Application.Builders;
using SImpl.Modules;
using SImpl.Runtime.Application.Builders;
using SImpl.Runtime.Core;

namespace SImpl.Hosts.WebHost.Application.Builders
{
    public class WebHostApplicationBuilder : SImplApplicationBuilder, IWebHostApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly IApplicationBootManager _bootManager;

        public WebHostApplicationBuilder(IModuleManager moduleManager, IApplicationBootManager bootManager) :
            base(moduleManager)
        {
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }
        
        public IWebHostApplicationBuilder UseWebHostAppModule<TModule>(Func<TModule> factory) 
            where TModule : IWebHostApplicationModule
        {
            _moduleManager.AttachModule(factory.Invoke());
            return this;
        }

        public TModule AttachNewWebHostAppModuleOrGetConfigured<TModule>(Func<TModule> factory) 
            where TModule : IWebHostApplicationModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public void Configure(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(this);
            
            _bootManager.Configure<IApplicationModule, ISImplApplicationBuilder>(this);
            _bootManager.Configure<IWebHostApplicationModule, IWebHostApplicationBuilder>(this);
        }

        public override ISImplApplication Build()
        {
            return new WebHostApplcation(_bootManager);
        }
    }
}