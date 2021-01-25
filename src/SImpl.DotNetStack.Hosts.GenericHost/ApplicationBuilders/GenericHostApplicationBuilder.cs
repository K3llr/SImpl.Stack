using System;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Application;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.ApplicationBuilders;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
{
    public class GenericHostApplicationBuilder : DotNetStackApplicationBuilder, IGenericHostApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly IApplicationBootManager _bootManager;
        
        public GenericHostApplicationBuilder(IModuleManager moduleManager, IApplicationBootManager bootManager) 
            : base(moduleManager)
        {
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }

        public void UseGenericHostStackAppModule<TModule>(Func<TModule> factory) 
            where TModule : IGenericHostApplicationModule
        {
            _moduleManager.AttachModule(factory.Invoke());
        }
        
        public TModule AttachNewGenericHostStackAppModuleOrGetConfigured<TModule>(Func<TModule> factory) 
            where TModule : IGenericHostApplicationModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public void Configure(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(this);
            
            _bootManager.Configure<IApplicationModule, IDotNetStackApplicationBuilder>(this);
            _bootManager.Configure<IGenericHostApplicationModule, IGenericHostApplicationBuilder>(this);
        }

        public override IDotNetStackApplication Build()
        {
            return new GenericHostApplication(_bootManager);
        }
    }
}