using System;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using Dapper;
using Dommel;
using static Dommel.DommelMapper;

namespace SImpl.Storage.Dapper.Helper
{
    public static class DapperHelper
    {
        internal static ConcurrentDictionary<QueryCacheKey, string> QueryCache { get; } = new();

        public static bool Delete<TEntity>(this IDbConnection connection, object id, IDbTransaction? transaction = null)
        {
            var sql = BuildDeleteByIdQuery(GetSqlBuilder(connection), typeof(TEntity));
            // LogQuery<TEntity>(sql);
            return connection.Execute(sql, id, transaction) > 0;
        }

        internal static string BuildDeleteByIdQuery(ISqlBuilder sqlBuilder, Type type)
        {
            var cacheKey = new QueryCacheKey(QueryCacheType.Delete, sqlBuilder, type);
            if (!QueryCache.TryGetValue(cacheKey, out var sql))
            {
                var tableName = Resolvers.Table(type, sqlBuilder);
                sql = $"delete from {tableName} where id = @0";

                QueryCache.TryAdd(cacheKey, sql);
            }

            return sql;
        }
    }
}