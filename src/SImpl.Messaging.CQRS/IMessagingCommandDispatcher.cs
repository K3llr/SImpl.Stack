using System.Collections.Generic;
using System.Threading.Tasks;
using SImpl.CQRS.Commands;

namespace SImpl.Messaging.CQRS
{
    public interface IMessagingCommandDispatcher: ICommandDispatcher
    {
        Task ExecuteAsync<TCommand>(TCommand command, IDictionary<string,string> headers)
            where TCommand : ICommand;
    }
}