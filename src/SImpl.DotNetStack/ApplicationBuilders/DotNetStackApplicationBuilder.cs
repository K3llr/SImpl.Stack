using System;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Exceptions;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public class DotNetStackApplicationBuilder : IDotNetStackApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly IApplicationBootManager _bootManager;

        public DotNetStackApplicationBuilder(IModuleManager moduleManager, IApplicationBootManager bootManager)
        {
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }

        public void Use<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule
        {
            var module = _moduleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                _moduleManager.AttachModule(factory.Invoke());
            }
            else
            {
                throw new InvalidConfigurationException($"A module with the type {nameof(TModule)} has already been attached to the stack.");
            }
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule
        {
            var module = _moduleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                module = factory.Invoke();
                _moduleManager.AttachModule(module);
            }

            return module;
        }

        public void Configure(Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(this);
            _bootManager.Configure(this);
        }
        
        public IDotNetStackApplication Build()
        {
            return new DotNetStackApplication(_bootManager);
        }
    }
}