using System.Threading.Tasks;
using SImpl.CQRS.Events;

namespace SImpl.CQRS.OpenTelemetry.Services.Events;

public sealed class EventHandlerInstrumentationDecorator<TEvent>(IEventHandler<TEvent> @base) : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public async Task HandleAsync(TEvent @event)
    {
        var type = @event.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"HandleAsync: {type.Name}");
        activity?.SetTag("event.fullname", type.FullName);
        await @base.HandleAsync(@event);
    }
}