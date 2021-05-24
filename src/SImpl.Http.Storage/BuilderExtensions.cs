using System;
using SImpl.Host.Builders;
using SImpl.Http.Storage.Module;

namespace SImpl.Http.Storage
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseTransactionalRequests(this ISImplHostBuilder host, Action<HttpStorageModuleConfig> configureDelegate)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new HttpStorageModule(new HttpStorageModuleConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}