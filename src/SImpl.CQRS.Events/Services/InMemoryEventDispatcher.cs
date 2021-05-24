using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SImpl.CQRS.Events.Services
{
    public class InMemoryEventDispatcher : IInMemoryEventDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public InMemoryEventDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        
        public async Task PublishAsync<T>(T @event) 
            where T : class, IEvent
        {
            using var scope = _serviceFactory.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<T>>();
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}