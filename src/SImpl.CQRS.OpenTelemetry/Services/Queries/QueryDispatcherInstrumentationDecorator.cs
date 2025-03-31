using System.Threading.Tasks;
using SImpl.CQRS.OpenTelemetry.Helpers;
using SImpl.CQRS.Queries;

namespace SImpl.CQRS.OpenTelemetry.Services.Queries;

public sealed class QueryDispatcherInstrumentationDecorator(IQueryDispatcher @base) : IInMemoryQueryDispatcher
{
    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        var type = query.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"QueryAsync: {type.Name}");
        activity?.SetTag("query.fullname", type.FullName);
        activity?.SetTag("query.serialized", SerializationHelper.SerializeActivityObject(query));
        return await @base.QueryAsync(query);
    }

    public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>
    {
        var type = query.GetType();
        using var activity = Constants.ActivitySource.StartActivity($"QueryAsync: {type.Name}");
        activity?.SetTag("query.fullname", type.FullName);
        activity?.SetTag("query.serialized", SerializationHelper.SerializeActivityObject(query));
        return await @base.QueryAsync<TQuery, TResult>(query);
    }
}