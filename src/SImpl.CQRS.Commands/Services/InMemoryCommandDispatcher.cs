using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SImpl.CQRS.Commands.Services
{
    public class InMemoryCommandDispatcher : IInMemoryCommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public InMemoryCommandDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        
        public async Task ExecuteAsync<T>(T command)
            where T : class, ICommand
        {
            using var scope = _serviceFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
            
            await handler.HandleAsync(command);
        }
    }
}