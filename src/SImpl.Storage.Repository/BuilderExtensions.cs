using SImpl.Host.Builders;
using SImpl.Storage.Repository.Module;

namespace SImpl.Storage.Repository
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseInMemoryRepositoryStorage(this ISImplHostBuilder host)
        {
            host.AttachNewOrGetConfiguredModule(() => new RepositoryModule());
            return host;
        }
    }
}