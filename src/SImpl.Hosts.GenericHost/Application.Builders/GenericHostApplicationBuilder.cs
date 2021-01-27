using System;
using SImpl.Application;
using SImpl.Application.Builders;
using SImpl.Modules;
using SImpl.Runtime.Application.Builders;
using SImpl.Runtime.Core;

namespace SImpl.Hosts.GenericHost.Application.Builders
{
    public class GenericHostApplicationBuilder : SImplApplicationBuilder, IGenericHostApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly IApplicationBootManager _bootManager;
        
        public GenericHostApplicationBuilder(IModuleManager moduleManager, IApplicationBootManager bootManager) 
            : base(moduleManager)
        {
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }

        public IGenericHostApplicationBuilder UseGenericHostAppModule<TModule>(Func<TModule> factory) 
            where TModule : IGenericHostApplicationModule
        {
            _moduleManager.AttachModule(factory.Invoke());
            return this;
        }
        
        public TModule AttachNewGenericHostAppModuleOrGetConfigured<TModule>(Func<TModule> factory) 
            where TModule : IGenericHostApplicationModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public void Configure(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(this);
            
            _bootManager.Configure<IApplicationModule, ISImplApplicationBuilder>(this);
            _bootManager.Configure<IGenericHostApplicationModule, IGenericHostApplicationBuilder>(this);
        }

        public override ISImplApplication Build()
        {
            return new GenericHostApplication(_bootManager);
        }
    }
}