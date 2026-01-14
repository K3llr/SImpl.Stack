using System.Threading.Tasks;
using SImpl.CQRS.Events;
using SImpl.CQRS.OpenTelemetry.Helpers;

namespace SImpl.CQRS.OpenTelemetry.Services.Events;

public class EventDispatcherInstrumentationDecorator : IInMemoryEventDispatcher
{
    private readonly IEventDispatcher _base;

    public EventDispatcherInstrumentationDecorator(IEventDispatcher @base)
    {
        _base = @base;
    }

    public async Task PublishAsync<T>(T @event) where T : class, IEvent
    {
        var type = @event.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"PublishAsync: {type.Name}");
        activity?.SetTag("event.fullname", type.FullName);
        activity?.SetTag("event.serialized", SerializationHelper.SerializeActivityObject(@event));
        await _base.PublishAsync(@event);
    }
}