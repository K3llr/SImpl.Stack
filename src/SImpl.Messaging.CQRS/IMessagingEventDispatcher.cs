using System.Collections.Generic;
using System.Threading.Tasks;
using SImpl.CQRS.Events;

namespace SImpl.Messaging.CQRS
{
    public interface IMessagingEventDispatcher : IEventDispatcher
    {
        Task PublishAsync<T>(T @event, IDictionary<string,string> headers) 
            where T : class, IEvent;
    }
}