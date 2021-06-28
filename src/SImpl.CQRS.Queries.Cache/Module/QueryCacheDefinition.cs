using System;

namespace SImpl.CQRS.Queries.Cache.Module
{
    public class QueryCacheDefinition
    {
        public Type QueryType { get; set; }
        public TimeSpan? TimeToLive { get; set; }
    }
}