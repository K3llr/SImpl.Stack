using System;
using SImpl.Host.Builders;
using SImpl.Storage.Repository.Module;

namespace SImpl.Storage.Repository
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseDapperRepositoryStorage(this ISImplHostBuilder host, Action<DapperRepositoryConfig> dapperRepoConfig = null)
        {
            var config = new DapperRepositoryConfig();
            if (dapperRepoConfig != null)
            {
                dapperRepoConfig.Invoke(config);
            }
            host.AttachNewOrGetConfiguredModule(() => new DapperRepositoryModule(config));
            return host;
        }
    }
}