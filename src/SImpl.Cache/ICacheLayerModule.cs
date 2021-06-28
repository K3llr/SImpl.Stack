using SImpl.Cache.Models;
using SImpl.Modules;

namespace SImpl.Cache
{
    public interface ICacheLayerModule : ISImplModule
    {
        public CacheLayerDefinition LayerDefinition { get; }
    }
}