using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SImpl.CQRS.Queries.Cache.Services
{
    public class QueryCacheKeyService : IQueryCacheKeyService
    {
        public string GenerateKey<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var serializedSourceObject = JsonSerializer.Serialize(new KeyObject<TQuery, TResult>
                {
                    SO = query,
                    ST = nameof(TQuery),
                    RT = nameof(TResult)
                },
                new JsonSerializerOptions
                {
                    WriteIndented = false
                });

            var jsonKey = HashKey(serializedSourceObject);

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

        private class KeyObject<TQuery, TResult>
            where TQuery : IQuery<TResult>
        {
            // SourceObject
            public TQuery SO { get; set; }
            
            // SourceType
            public string ST { get; set; }
            
            // ResultType
            public string RT { get; set; }
        }
    }
}