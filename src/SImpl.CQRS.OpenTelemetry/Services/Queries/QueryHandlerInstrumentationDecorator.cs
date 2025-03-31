using System.Threading.Tasks;
using SImpl.CQRS.OpenTelemetry.Helpers;
using SImpl.CQRS.Queries;

namespace SImpl.CQRS.OpenTelemetry.Services.Queries;

public class QueryHandlerInstrumentationDecorator<TQuery, TResult>(IQueryHandler<TQuery, TResult> @base)
    : IQueryHandler<TQuery, TResult> where TQuery : class, IQuery<TResult>
{
    public async Task<TResult> HandleAsync(TQuery query)
    {
        var type = query.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"HandleAsync: {type.Name}");
        activity?.SetTag("query.fullname", type.FullName);
        activity?.SetTag("query.serialized", SerializationHelper.SerializeActivityObject(query));
        return await @base.HandleAsync(query);
    }
}