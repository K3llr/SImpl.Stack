using SImpl.Host.Builders;
using SImpl.Storage.KeyValue.Module;

namespace SImpl.Storage.KeyValue
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseInMemoryKeyValueStorage(this ISImplHostBuilder host)
        {
            host.AttachNewOrGetConfiguredModule(() => new KeyValueModule());
            
            return host;
        }
    }
}