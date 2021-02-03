using System;
using System.Collections.Generic;
using System.Reflection;

namespace SImpl.CQRS.Commands.Module
{
    public class CommandModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<Type> _queryHandlers = new();
        
        public bool EnableInMemoryCommandDispatcher { get; set; }
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        public IReadOnlyList<Type> RegisteredQueryHandlers => _queryHandlers.AsReadOnly();
        
        public CommandModuleConfig AddCommandHandlersFromAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }
        
        public CommandModuleConfig AddCommandHandlersFromAssemblyOf<T>()
        {
            AddCommandHandlersFromAssembly(typeof(T).Assembly);
            return this;
        }
        
        // TODO:
        /*public CommandModuleConfig AddCommandHandler<TQueryHandler>()
            where TQueryHandler : IQueryHandler
        {
            _queryHandlers.Add(typeof(TQueryHandler));
            return this;
        }*/
    }
}