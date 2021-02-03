using System.Threading.Tasks;

namespace SImpl.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task ExecuteAsync<T>(T command)
            where T : class, ICommand;
    }
}