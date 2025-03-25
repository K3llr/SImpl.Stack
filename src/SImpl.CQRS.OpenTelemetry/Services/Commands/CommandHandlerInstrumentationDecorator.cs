using System.Threading.Tasks;
using SImpl.CQRS.Commands;

namespace SImpl.CQRS.OpenTelemetry.Services.Commands;

public sealed class CommandHandlerInstrumentationDecorator<T>(ICommandHandler<T> @base) : ICommandHandler<T> where T : class, ICommand
{
    public async Task HandleAsync(T command)
    {
        var type = command.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"HandleAsync: {type.Name}");
        activity?.SetTag("command.fullname", type.FullName);
        await @base.HandleAsync(command);
    }
}