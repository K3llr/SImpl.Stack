using System.Threading.Tasks;
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
        return await @base.HandleAsync(query);
    }
}