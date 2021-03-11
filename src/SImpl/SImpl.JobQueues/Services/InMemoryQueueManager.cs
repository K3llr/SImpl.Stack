using Microsoft.Extensions.DependencyInjection;

namespace SImpl.JobQueues.Services
{
    public class InMemoryQueueManager : IInMemoryQueueManager
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public InMemoryQueueManager(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        public void Enqueue<T>(T job)
        {
            using var scope = _serviceFactory.CreateScope();
            
            var queueType = typeof(IQueue<>).MakeGenericType(typeof(T));
            var queue = scope.ServiceProvider.GetRequiredService(queueType);
            queueType
                .GetMethod(nameof(IQueue<T>.Enqueue))?
                .Invoke(queue, new object[] {job});
        }
    }
}