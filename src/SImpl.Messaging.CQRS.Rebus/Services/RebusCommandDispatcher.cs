using System.Collections.Generic;
using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Transport;
using SImpl.CQRS.Commands;

namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class RebusCommandDispatcher : IMessagingCommandDispatcher
    {
        private readonly IBus _bus;

        public RebusCommandDispatcher(IBus bus)
        {
            _bus = bus;
        }
        
        public Task ExecuteAsync<T>(T command)
            where T : class, ICommand
        {
            return ExecuteAsync(command, new Dictionary<string, string>());
        }

        public async Task ExecuteAsync<TCommand>(TCommand command, IDictionary<string, string> headers)
            where TCommand : ICommand
        {
            using var scope = new RebusTransactionScope();
            await _bus.Send(command, headers);
            await scope.CompleteAsync();
        }
    }
}