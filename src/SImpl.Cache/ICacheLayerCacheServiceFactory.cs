using SImpl.Cache.Models;

namespace SImpl.Cache
{
    public interface ICacheLayerCacheServiceFactory
    {
        ICacheService Create(CacheLayerDefinition definition);
    }
}