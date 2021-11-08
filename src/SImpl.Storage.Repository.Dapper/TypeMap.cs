using System;

namespace SImpl.Storage.Repository.Dapper
{
    public class TypeMap
    {
        public Type Type { get; set; }
        public string TableName { get; set; }
        public string[] Keys { get; set; } = Array.Empty<string>();
        public string[] ExplicitKeys { get; set; } = Array.Empty<string>();
        public string[] ComputedProperties { get; set; } = Array.Empty<string>();
    }
}