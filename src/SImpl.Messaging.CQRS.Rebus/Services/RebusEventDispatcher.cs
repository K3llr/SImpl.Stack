using System.Collections.Generic;
using System.Threading.Tasks;
using Rebus.Bus;
using SImpl.CQRS.Events;

namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class RebusEventDispatcher : IMessagingEventDispatcher
    {
        private readonly IBus _bus;

        public RebusEventDispatcher(IBus bus)
        {
            _bus = bus;
        }
        
        public async Task PublishAsync<T>(T @event) 
            where T : class, IEvent
        {
            await PublishAsync(@event, new Dictionary<string, string>());
        }

        public async Task PublishAsync<T>(T @event, IDictionary<string, string> headers) 
            where T : class, IEvent
        {
            await _bus.Publish(@event, headers);
        }
    }
}