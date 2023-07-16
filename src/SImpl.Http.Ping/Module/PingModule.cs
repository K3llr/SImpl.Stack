using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Hosts.WebHost.Modules;
using SImpl.Http.Ping.AspNetCore;

namespace SImpl.Http.Ping.Module;

public class PingModule: IAspNetPostModule
{
    public string Name => nameof(PingModule);

    public static PingModuleConfig ModuleConfig { get; private set; } = new();
    public static bool Enabled { get; private set; }
        
    public PingModule(PingModuleConfig moduleConfig)
    {
        ModuleConfig = moduleConfig;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        Enabled = ModuleConfig.Predicate?.Invoke(env) ?? false;
        if (Enabled)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    ModuleConfig.ControllerArea,
                    ModuleConfig.ControllerArea,
                    $"{ModuleConfig.RoutePrefix}/{{controller=Ping}}/{{action=Ping}}");
            });
        }
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c => { c.DocumentFilter<PingControllerSwaggerDocumentFilter>(); });
    }
}