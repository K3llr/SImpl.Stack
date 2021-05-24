using System.Threading.Tasks;
using Rebus.Handlers;
using SImpl.CQRS.Events;

namespace SImpl.Messaging.CQRS.Services
{
    public class EventMessageHandler<TEvent> : IHandleMessages<TEvent>
        where TEvent : class, IEvent
    {
        private readonly IInMemoryEventDispatcher _eventDispatcher;

        public EventMessageHandler(IInMemoryEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public async Task Handle(TEvent @event)
        {
            await _eventDispatcher.PublishAsync(@event);
        }
    }
}