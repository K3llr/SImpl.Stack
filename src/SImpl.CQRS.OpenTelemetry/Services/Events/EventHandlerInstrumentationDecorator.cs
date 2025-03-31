using System.Threading.Tasks;
using SImpl.CQRS.Events;
using SImpl.CQRS.OpenTelemetry.Helpers;

namespace SImpl.CQRS.OpenTelemetry.Services.Events;

public sealed class EventHandlerInstrumentationDecorator<TEvent>(IEventHandler<TEvent> @base) : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public async Task HandleAsync(TEvent @event)
    {
        var type = @event.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"HandleAsync: {type.Name}");
        activity?.SetTag("event.fullname", type.FullName);
        activity?.SetTag("event.serialized", SerializationHelper.SerializeActivityObject(@event));
        await @base.HandleAsync(@event);
    }
}