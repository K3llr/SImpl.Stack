using System.Threading.Tasks;
using Rebus.Handlers;
using SImpl.CQRS.Commands;

namespace SImpl.Messaging.CQRS.Services
{
    public class CommandMessageHandler<TCommand> : IHandleMessages<TCommand>
        where TCommand : class, ICommand
    {
        private readonly IInMemoryCommandDispatcher _commandDispatcher;

        public CommandMessageHandler(IInMemoryCommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task Handle(TCommand command)
        {
            await _commandDispatcher
                .ExecuteAsync(command);
        }
    }
}