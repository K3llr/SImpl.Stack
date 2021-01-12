using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseApplicationBootManager : IApplicationBootManager
    {
        private readonly IApplicationBootManager _bootManager;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseApplicationBootManager(IApplicationBootManager bootManager, ILogger<VerboseHost> logger)
        {
            _bootManager = bootManager;
            _logger = logger;
        }
        
        public void Configure(IDotNetStackApplicationBuilder appBuilder)
        {
            _logger.LogDebug("    ApplicationBootManager > Configure started");
            _bootManager.Configure(appBuilder);
            _logger.LogDebug("    ApplicationBootManager > Configure ended");
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            _logger.LogDebug("    ApplicationBootManager > ConfigureServices started");
            _bootManager.ConfigureServices(serviceCollection);
            _logger.LogDebug("    ApplicationBootManager> ConfigureServices ended");
        }

        public async Task StartAsync()
        {
            _logger.LogDebug("    ApplicationBootManager > StartAsync started");
            await _bootManager.StartAsync();
            _logger.LogDebug("    ApplicationBootManager > StartAsync ended");
        }

        public async Task StopAsync()
        {
            _logger.LogDebug("    ApplicationBootManager > StopAsync started");
            await _bootManager.StopAsync();
            _logger.LogDebug("    ApplicationBootManager > StopAsync ended");
        }
    }
}