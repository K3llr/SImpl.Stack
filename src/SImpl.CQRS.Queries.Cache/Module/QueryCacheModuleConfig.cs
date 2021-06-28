using System;
using System.Collections.Generic;

namespace SImpl.CQRS.Queries.Cache.Module
{
    public class QueryCacheModuleConfig
    {
        private readonly IDictionary<Type, QueryCacheDefinition> _definitions = new Dictionary<Type, QueryCacheDefinition>();

        public QueryCacheDefinition GetQueryCacheDefinition(Type queryType)
        {
            return _definitions.ContainsKey(queryType) 
                ? _definitions[queryType] 
                : null;
        }

        public QueryCacheModuleConfig RegisterQueryCacheDefinition(params QueryCacheDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                _definitions.Add(definition.QueryType, definition);    
            }

            return this;
        }
    }
}