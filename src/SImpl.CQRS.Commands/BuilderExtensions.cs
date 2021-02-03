using System;
using SImpl.CQRS.Commands.Module;
using SImpl.Host.Builders;

namespace SImpl.CQRS.Commands
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseInMemoryCommandDispatcher(this ISImplHostBuilder host)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new CommandModule(new CommandModuleConfig()));
            module.Config.EnableInMemoryCommandDispatcher = true;
            return host;
        }

        public static ISImplHostBuilder UseCommands(this ISImplHostBuilder host, Action<CommandModuleConfig> configureDelegate)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new CommandModule(new CommandModuleConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}