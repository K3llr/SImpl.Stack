using Microsoft.Extensions.DependencyInjection;
using SImpl.CQRS.Commands.Services;
using SImpl.Modules;

namespace SImpl.CQRS.Commands.Module
{
    public class CommandModule : IServicesCollectionConfigureModule
    {
        public CommandModuleConfig Config { get; }

        public CommandModule(CommandModuleConfig config)
        {
            Config = config;
        }

        public string Name { get; } = nameof(CommandModule);
        public void ConfigureServices(IServiceCollection services)
        {
            if (Config.EnableInMemoryCommandDispatcher)
            {
                services.AddSingleton<IInMemoryCommandDispatcher, InMemoryCommandDispatcher>();
            }
            
            services.Scan(s =>
                s.FromAssemblies(Config.RegisteredAssemblies)
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            
            var handlerInterface = typeof(ICommandHandler<>);
            foreach (var handlerReg in Config.RegisteredCommandHandlers)
            {
                services.AddTransient(handlerInterface.MakeGenericType(handlerReg.CommandType),
                    handlerReg.CommandHandlerType);
            }
        }
    }
}
