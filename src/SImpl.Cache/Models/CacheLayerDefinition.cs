using SImpl.Common;

namespace SImpl.Cache.Models
{
    public class CacheLayerDefinition
    {  
        public TypeOf<ICacheService> CacheServiceType { get; set; }

        public CacheLayerOptions Options { get; set; } = new();
    }
}