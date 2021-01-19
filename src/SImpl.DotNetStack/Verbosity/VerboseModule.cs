using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseModule : IPreInitModule, IServicesCollectionConfigureModule, IHostBuilderConfigureModule, IHostConfigureModule, IStartableModule, IApplicationModule, IDiagnosticsModule
    {
        private readonly IDotNetStackModule _module;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseModule(IDotNetStackModule module, ILogger<VerboseHost> logger)
        {
            _module = module;
            _logger = logger;
        }

        public string Name => _module.Name;
        
        public void Diagnose(IDiagnosticsCollector collector)
        {
            if (_module is IDiagnosticsModule module)
            {
                _logger.LogDebug($"  IDiagnosticsModule > {_module.GetType().Name} > Diagnose");
                module.Diagnose(collector);
            }
        }

        public void ConfigureHost(IHost host)
        {
            if (_module is IHostConfigureModule module)
            {
                _logger.LogDebug($"  IHostConfigureModule > {_module.GetType().Name} > ConfigureHost");
                module.ConfigureHost(host);
            }
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            if (_module is IHostBuilderConfigureModule module)
            {
                _logger.LogDebug($"  IHostBuilderConfigureModule > {_module.GetType().Name} > ConfigureHostBuilder");
                module.ConfigureHostBuilder(hostBuilder);
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_module is IServicesCollectionConfigureModule module)
            {
                _logger.LogDebug($"  IServicesCollectionConfigureModule > {_module.GetType().Name} > ConfigureServices");
                module.ConfigureServices(services);
            }
        }

        public void PreInit()
        {
            if (_module is IPreInitModule module)
            {
                _logger.LogDebug($"  IPreInitModule > {_module.GetType().Name} > PreInit");
                module.PreInit();
            }
        }

        public void Configure(IDotNetStackApplicationBuilder builder)
        {
            if (_module is IApplicationModule module)
            {
                _logger.LogDebug($"     IApplicationModule > {_module.GetType().Name} > Configure");
                module.Configure(builder);
            }
        }

        async Task IApplicationModule.StartAsync()
        {
            if (_module is IApplicationModule module)
            {
                _logger.LogDebug($"     IApplicationModule > {_module.GetType().Name} > StartAsync");
                await module.StartAsync();
            }
        }

        async Task IApplicationModule.StopAsync()
        {
            if (_module is IApplicationModule module)
            {
                _logger.LogDebug($"     IApplicationModule > {_module.GetType().Name} > StopAsync");
                await module.StopAsync();
            }
        }

        async Task IStartableModule.StartAsync()
        {
            if (_module is IStartableModule module)
            {
                _logger.LogDebug($"  IStartableModule > {_module.GetType().Name} > StartAsync");
                await module.StartAsync();
            }
        }

        async Task IStartableModule.StopAsync()
        {
            if (_module is IStartableModule module)
            {
                _logger.LogDebug($"  IStartableModule > {_module.GetType().Name} > StopAsync");
                await module.StopAsync();
            }
        }
    }
}