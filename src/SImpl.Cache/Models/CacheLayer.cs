using System;

namespace SImpl.Cache.Models
{
    public class CacheLayer
    {
        public ICacheService CacheService { get; set; }

        public CacheLayerOptions Options { get; set; } = new();
    }
}