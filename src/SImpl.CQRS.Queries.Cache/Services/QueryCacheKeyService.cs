using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace SImpl.CQRS.Queries.Cache.Services
{
    public class QueryCacheKeyService : IQueryCacheKeyService
    {
        public string GenerateKey<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var serializedSourceObject = JsonSerializer.Serialize(new KeyObject
                {
                    SourceObject = query,
                    SourceType = typeof(TQuery).FullName,
                    ResultType = typeof(TResult).FullName
                },
                new JsonSerializerOptions
                {
                    WriteIndented = false
                });
            
            var hashedKey = HashKey(serializedSourceObject);

            return hashedKey;
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
            // SourceObject
            public object SourceObject { get; set; }
            
            // SourceType
            public string SourceType { get; set; }
            
            // ResultType
            public string ResultType { get; set; }
        }
    }
}