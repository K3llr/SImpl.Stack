using System.Threading.Tasks;
using SImpl.CQRS.Commands;
using SImpl.CQRS.OpenTelemetry.Helpers;

namespace SImpl.CQRS.OpenTelemetry.Services.Commands;

public sealed class CommandDispatcherInstrumentationDecorator(ICommandDispatcher @base) : IInMemoryCommandDispatcher
{
    public async Task ExecuteAsync<T>(T command) where T : class, ICommand
    {
        var type = command.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"ExecuteAsync: {type.Name}");
        activity?.SetTag("command.fullname", type.FullName);
        activity?.SetTag("command.serialized", SerializationHelper.SerializeActivityObject(command));
        await @base.ExecuteAsync(command);
    }
}