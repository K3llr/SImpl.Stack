using Microsoft.Extensions.Hosting;

namespace SImpl.Http.Ping.Module;

public class PingModuleConfig
{
    public Predicate<IHostEnvironment> Predicate { get; private set; } = env => env.IsDevelopment();
    public string RoutePrefix { get; private set; } = string.Empty;
    public string Version { get; private set; } = "v0.0.1";
    public string ControllerArea { get; private set; } = "Ping";

    public PingModuleConfig EnableWhen(Predicate<IHostEnvironment> predicate)
    {
        Predicate = predicate;
        return this;
    }
        
    public PingModuleConfig WithRoutePrefix(string routePrefix)
    {
        RoutePrefix = routePrefix;
        return this;
    }
        
    public PingModuleConfig WithVersion(string version)
    {
        Version = version;
        return this;
    }
        
    public PingModuleConfig WithControllerArea(string area)
    {
        ControllerArea = area;
        return this;
    }
}