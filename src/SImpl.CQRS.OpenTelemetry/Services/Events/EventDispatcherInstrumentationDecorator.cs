using System.Threading.Tasks;
using SImpl.CQRS.Events;

namespace SImpl.CQRS.OpenTelemetry.Services.Events;

public class EventDispatcherInstrumentationDecorator(IEventDispatcher @base) : IInMemoryEventDispatcher
{
    public async Task PublishAsync<T>(T @event) where T : class, IEvent
    {
        var type = @event.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"PublishAsync: {type.Name}");
        activity?.SetTag("event.fullname", type.FullName);
        await @base.PublishAsync(@event);
    }
}