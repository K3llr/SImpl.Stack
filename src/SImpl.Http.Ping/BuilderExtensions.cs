using SImpl.Host.Builders;
using SImpl.Hosts.WebHost;
using SImpl.Hosts.WebHost.Host.Builders;
using SImpl.Http.Ping.Module;

namespace SImpl.Http.Ping;

public static class BuilderExtensions
{
    public static IWebHostBuilder UseHealthCheckEndpoints(this IWebHostBuilder hostBuilder, Action<PingModuleConfig> configureDelegate)
    {
        var module = hostBuilder.AttachNewWebHostModuleOrGetConfigured(() => new PingModule(new PingModuleConfig()));
        configureDelegate?.Invoke(PingModule.ModuleConfig);
            
        return hostBuilder;
    }
        
    public static ISImplHostBuilder UseCommonHostHealthModule(this ISImplHostBuilder hostBuilder, Action<PingModuleConfig> configureDelegate)
    {
        hostBuilder.ConfigureWebHostStackApp(web =>
        {
            web.UseHealthCheckEndpoints(configureDelegate);
        });
            
        return hostBuilder;
    }
}