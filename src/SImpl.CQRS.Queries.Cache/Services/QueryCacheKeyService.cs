using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SImpl.CQRS.Queries.Cache.Services
{
    public class QueryCacheKeyService : IQueryCacheKeyService
    {
        public string GenerateKey<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var serializedQuery = JsonSerializer.Serialize(new KeyObject
            {
                Q = query,
                N = nameof(TResult)
            });

            var jsonKey = HashKey(serializedQuery);

            return jsonKey;
        }

        private string HashKey(string key)
        {
            using (var hasher = SHA256.Create())
            {
                var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(key))
                    .Select(b => b.ToString("x2"));

                return string.Join("", bytes);
            }
        }

        private class KeyObject
        {
            public object Q { get; set; }
            public string N { get; set; }
        }
    }
}