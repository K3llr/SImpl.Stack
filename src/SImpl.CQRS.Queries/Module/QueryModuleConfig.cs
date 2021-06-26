using System;
using System.Collections.Generic;
using System.Reflection;

namespace SImpl.CQRS.Queries.Module
{
    public class QueryModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<QueryHandlerRegistration> _queryHandlers = new();

        public bool EnableInMemoryQueryDispatcher { get; set; }
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        public IReadOnlyList<QueryHandlerRegistration> RegisteredQueryHandlers => _queryHandlers.AsReadOnly();
        
        public QueryModuleConfig AddQueryHandlersFromAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }
        
        public QueryModuleConfig AddQueryHandlersFromAssemblyOf<T>()
        {
            AddQueryHandlersFromAssembly(typeof(T).Assembly);
            return this;
        }

        public QueryModuleConfig AddQueryHandler<TQueryHandler, TQuery, TResult>()
            where TQueryHandler : IQueryHandler<TQuery, TResult>
            where TQuery : class, IQuery<TResult>
        {
            _queryHandlers.Add(new QueryHandlerRegistration
            {
                QueryType = typeof(TQuery), 
                ResultType = typeof(TResult), 
                QueryHandlerType = typeof(TQueryHandler)
            });
            return this;
        }
    }

    public class QueryHandlerRegistration
    {
        public Type QueryType { get; set; }
        public Type ResultType { get; set; }
        public Type QueryHandlerType { get; set; }
    }
}