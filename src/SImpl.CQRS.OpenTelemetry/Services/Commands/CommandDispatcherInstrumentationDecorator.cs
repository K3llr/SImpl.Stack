using System.Threading.Tasks;
using SImpl.CQRS.Commands;

namespace SImpl.CQRS.OpenTelemetry.Services.Commands;

public sealed class CommandDispatcherInstrumentationDecorator(ICommandDispatcher @base) : IInMemoryCommandDispatcher
{
    public async Task ExecuteAsync<T>(T command) where T : class, ICommand
    {
        var type = command.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"ExecuteAsync: {type.Name}");
        activity?.SetTag("command.fullname", type.FullName);
        await @base.ExecuteAsync(command);
    }
}