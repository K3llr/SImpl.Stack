using System;
using SImpl.Host.Builders;
using SImpl.Storage.Repository.Dapper.Module;

namespace SImpl.Storage.Repository.Dapper
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseDapperRepositoryStorage(this ISImplHostBuilder host, Action<DapperRepositoryConfig> dapperRepoConfig = null)
        {
            var config = new DapperRepositoryConfig();
            dapperRepoConfig?.Invoke(config);
            
            host.AttachNewOrGetConfiguredModule(() => new DapperRepositoryModule(config));
            
            return host;
        }
    }
}