using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Host;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseHostBootManager : IHostBootManager
    {
        private readonly IHostBootManager _bootManager;
        private readonly IModuleManager _moduleManager;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseHostBootManager(IHostBootManager bootManager, IModuleManager moduleManager, ILogger<VerboseHost> logger)
        {
            _bootManager = bootManager;
            _moduleManager = moduleManager;
            _logger = logger;
        }

        public void PreInit()
        {
            _logger.LogDebug(" HostBootManager > Module boot order");
            foreach (var module in _moduleManager.BootSequence)
            {
                _logger.LogDebug($"  - {module.Name}");
            }
            
            _logger.LogDebug(" HostBootManager > PreInit started");
            _bootManager.PreInit();
            _logger.LogDebug(" HostBootManager > PreInit ended");
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            _logger.LogDebug(" HostBootManager > ConfigureServices started");
            _bootManager.ConfigureServices(hostBuilder);
            _logger.LogDebug(" HostBootManager > ConfigureServices ended");
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            _logger.LogDebug(" HostBootManager > ConfigureHostBuilder started");
            _bootManager.ConfigureHostBuilder(hostBuilder);
            _logger.LogDebug(" HostBootManager > ConfigureHostBuilder ended");
        }

        public void ConfigureHost(IHost host)
        {
            _logger.LogDebug(" HostBootManager > ConfigureHost started");
            _bootManager.ConfigureHost(host);
            _logger.LogDebug(" HostBootManager > ConfigureHost ended");
        }

        public async Task StartAsync()
        {
            _logger.LogDebug(" HostBootManager > Module boot order");
            foreach (var module in _moduleManager.BootSequence)
            {
                _logger.LogDebug($"  - {module.Name}");
            }
            
            _logger.LogDebug(" HostBootManager > StartAsync started"); 
            await _bootManager.StartAsync();
            _logger.LogDebug(" HostBootManager > StartAsync ended");
        }

        public async Task StopAsync()
        {
            _logger.LogDebug(" HostBootManager > Module boot order");
            foreach (var module in _moduleManager.BootSequence)
            {
                _logger.LogDebug($"  - {module.Name}");
            }
            
            _logger.LogDebug(" HostBootManager > StopAsync started"); 
            await _bootManager.StopAsync();
            _logger.LogDebug(" HostBootManager > StopAsync ended");
        }
    }
}