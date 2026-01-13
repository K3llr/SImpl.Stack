using System.Threading.Tasks;
using SImpl.CQRS.Events;
using SImpl.CQRS.OpenTelemetry.Helpers;

namespace SImpl.CQRS.OpenTelemetry.Services.Events;

public sealed class EventHandlerInstrumentationDecorator<TEvent> : IEventHandler<TEvent> where TEvent : class, IEvent
{
    private readonly IEventHandler<TEvent> _base;

    public EventHandlerInstrumentationDecorator(IEventHandler<TEvent> @base)
    {
        _base = @base;
    }

    public async Task HandleAsync(TEvent @event)
    {
        var type = @event.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"HandleAsync: {type.Name}");
        activity?.SetTag("event.fullname", type.FullName);
        activity?.SetTag("event.serialized", SerializationHelper.SerializeActivityObject(@event));
        await _base.HandleAsync(@event);
    }
}