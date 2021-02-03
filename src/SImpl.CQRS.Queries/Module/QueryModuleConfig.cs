using System;
using System.Collections.Generic;
using System.Reflection;

namespace SImpl.CQRS.Queries.Module
{
    public class QueryModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<Type> _queryHandlers = new();

        public bool EnableInMemoryQueryDispatcher { get; set; }
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        public IReadOnlyList<Type> RegisteredQueryHandlers => _queryHandlers.AsReadOnly();
        
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
        
        // TODO:
        /*public QueryModuleConfig AddQueryHandler<TQueryHandler>()
            where TQueryHandler : IQueryHandler
        {
            _queryHandlers.Add(typeof(TQueryHandler));
            return this;
        }*/
    }
}