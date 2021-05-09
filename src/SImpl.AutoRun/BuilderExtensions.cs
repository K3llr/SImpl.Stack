using System;
using SImpl.AutoRun.Module;
using SImpl.Host.Builders;

namespace SImpl.AutoRun
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseAutoRunModules(this ISImplHostBuilder host, Action<AutoRunModuleConfig> configureDelegate = null)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new AutoRunModule(host, new AutoRunModuleConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}