using System;
using SImpl.Host.Builders;
using SImpl.Storage.Repository.Module;
using SImpl.Storage.Repository.NPoco.Module;

namespace SImpl.Storage.Repository
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseNPocoRepositoryStorage(this ISImplHostBuilder host, Action<NPocoRepositoryConfig> dapperRepoConfig = null)
        {
            var config = new NPocoRepositoryConfig();
            if (dapperRepoConfig != null)
            {
                dapperRepoConfig.Invoke(config);
            }
            host.AttachNewOrGetConfiguredModule(() => new NPocoRepositoryModule(config));
            return host;
        }
    }
}