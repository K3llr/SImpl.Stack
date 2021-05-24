using System.Threading.Tasks;

namespace SImpl.CQRS.Events
{
    public interface IEventDispatcher
    {
        Task PublishAsync<T>(T @event) 
            where T : class, IEvent;
    }
}