using System.Threading.Tasks;

namespace SImpl.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> 
        where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}