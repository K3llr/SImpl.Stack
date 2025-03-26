using System;
using SImpl.CQRS.OpenTelemetry.Module;
using SImpl.Host.Builders;

namespace SImpl.CQRS.OpenTelemetry;

public static class BuilderExtensions
{
    public static ISImplHostBuilder UseCqrsOpenTelemetry(this ISImplHostBuilder host)
    {
        host.UseCqrsOpenTelemetry(config =>
        {
            config.EnableCommandsInstrumentation = true;
            config.EnableQueriesInstrumentation = true;
            config.EnableEventsInstrumentation = true;
        });
            
        return host;
    }
    
    public static ISImplHostBuilder UseCqrsOpenTelemetry(this ISImplHostBuilder host, Action<CqrsOpenTelemetryModuleConfig> configureDelegate)
    {
        var module = host.AttachNewOrGetConfiguredModule(() => new CqrsOpenTelemetryModule(new CqrsOpenTelemetryModuleConfig()));
        configureDelegate?.Invoke(module.Config);
            
        return host;
    }
}