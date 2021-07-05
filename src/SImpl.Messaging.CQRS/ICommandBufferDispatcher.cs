using System.Threading.Tasks;
using SImpl.CQRS.Commands;

namespace SImpl.Messaging.CQRS
{
    public interface ICommandBufferDispatcher<TCommand>
        where TCommand : class, ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}