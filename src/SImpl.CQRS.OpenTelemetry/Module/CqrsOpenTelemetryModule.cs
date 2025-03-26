using Microsoft.Extensions.DependencyInjection;
using SImpl.CQRS.Commands;
using SImpl.CQRS.Events;
using SImpl.CQRS.OpenTelemetry.Services.Commands;
using SImpl.CQRS.OpenTelemetry.Services.Events;
using SImpl.CQRS.OpenTelemetry.Services.Queries;
using SImpl.CQRS.Queries;
using SImpl.Modules;

namespace SImpl.CQRS.OpenTelemetry.Module;

public class CqrsOpenTelemetryModule(CqrsOpenTelemetryModuleConfig config) : IServicesCollectionConfigureModule
{
    public string Name => nameof(CqrsOpenTelemetryModule);

    public CqrsOpenTelemetryModuleConfig Config { get; } = config;

    public void ConfigureServices(IServiceCollection services)
    {
        if (Config.EnableCommandsInstrumentation)
        {
            services.TryDecorate<IInMemoryCommandDispatcher, CommandDispatcherInstrumentationDecorator>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(CommandHandlerInstrumentationDecorator<>));
            if (!Config.EnableOnlyForInMemoryImplementations)
            {
                services.TryDecorate<ICommandDispatcher, CommandDispatcherInstrumentationDecorator>();
            }
        }

        if (Config.EnableEventsInstrumentation)
        {
            services.TryDecorate<IInMemoryEventDispatcher, EventDispatcherInstrumentationDecorator>();
            services.TryDecorate(typeof(IEventHandler<>), typeof(EventHandlerInstrumentationDecorator<>));
            if (!Config.EnableOnlyForInMemoryImplementations)
            {
                services.TryDecorate<IEventDispatcher, EventDispatcherInstrumentationDecorator>();
            }
        }

        if (Config.EnableQueriesInstrumentation)
        {
            services.TryDecorate<IInMemoryQueryDispatcher, QueryDispatcherInstrumentationDecorator>();
            services.TryDecorate(typeof(IQueryHandler<,>), typeof(QueryHandlerInstrumentationDecorator<,>));

            if (!Config.EnableOnlyForInMemoryImplementations)
            {
                services.TryDecorate<IQueryDispatcher, QueryDispatcherInstrumentationDecorator>();
            }
        }
    }
}