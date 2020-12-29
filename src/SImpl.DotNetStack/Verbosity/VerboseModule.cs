using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseModule : IPreInitModule, IServicesCollectionConfigureModule, IHostBuilderConfigureModule, IHostConfigureModule, IStartableModule, IApplicationModule
    {
        private readonly IDotNetStackModule _module;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseModule(IDotNetStackModule module, ILogger<VerboseHost> logger)
        {
            _module = module;
            _logger = logger;
        }

        public string Name => _module.Name;
        
        public void ConfigureHost(IHost host)
        {
            if (_module is IHostConfigureModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.ConfigureHost");
                module.ConfigureHost(host);
            }
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            if (_module is IHostBuilderConfigureModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.ConfigureHostBuilder");
                module.ConfigureHostBuilder(hostBuilder);
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_module is IServicesCollectionConfigureModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.ConfigureServices");
                module.ConfigureServices(services);
            }
        }

        public void PreInit()
        {
            if (_module is IPreInitModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.PreInit");
                module.PreInit();
            }
        }

        public void ConfigureApplication(IDotNetStackApplicationBuilder builder)
        {
            if (_module is IApplicationModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.ConfigureApplication");
                module.ConfigureApplication(builder);
            }
        }

        async Task IApplicationModule<IDotNetStackApplicationBuilder>.StartAsync()
        {
            if (_module is IApplicationModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.StartAsync");
                await module.StartAsync();
            }
        }

        async Task IApplicationModule<IDotNetStackApplicationBuilder>.StopAsync()
        {
            if (_module is IApplicationModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.StopAsync");
                await module.StopAsync();
            }
        }

        async Task IStartableModule.StartAsync()
        {
            if (_module is IStartableModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.StartAsync");
                await module.StartAsync();
            }
        }

        async Task IStartableModule.StopAsync()
        {
            if (_module is IStartableModule module)
            {
                _logger.LogDebug($"- {_module.GetType().Name}.StopAsync");
                await module.StopAsync();
            }
        }
    }
}