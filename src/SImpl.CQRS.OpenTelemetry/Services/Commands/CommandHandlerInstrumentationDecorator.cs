using System.Threading.Tasks;
using SImpl.CQRS.Commands;
using SImpl.CQRS.OpenTelemetry.Helpers;

namespace SImpl.CQRS.OpenTelemetry.Services.Commands;

public sealed class CommandHandlerInstrumentationDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _base;

    public CommandHandlerInstrumentationDecorator(ICommandHandler<T> @base)
    {
        _base = @base;
    }

    public async Task HandleAsync(T command)
    {
        var type = command.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"HandleAsync: {type.Name}");
        activity?.SetTag("command.fullname", type.FullName);
        activity?.SetTag("command.serialized", SerializationHelper.SerializeActivityObject(command));
        await _base.HandleAsync(command);
    }
}