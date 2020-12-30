using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Modules;

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
            _logger.LogDebug("> Module boot order");
            foreach (var module in _moduleManager.BootSequence)
            {
                _logger.LogDebug($"   - {module.Name}");
            }
            
            _logger.LogDebug("> PreInit started");
            _bootManager.PreInit();
            _logger.LogDebug("> PreInit ended");
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            _logger.LogDebug("> ConfigureServices started");
            _bootManager.ConfigureServices(hostBuilder);
            _logger.LogDebug("> ConfigureServices ended");
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            _logger.LogDebug("> ConfigureHostBuilder started");
            _bootManager.ConfigureHostBuilder(hostBuilder);
            _logger.LogDebug("> ConfigureHostBuilder ended");
        }

        public void ConfigureHost(IHost host)
        {
            _logger.LogDebug("> ConfigureHost started");
            _bootManager.ConfigureHost(host);
            _logger.LogDebug("> ConfigureHost ended");
        }

        public async Task StartAsync()
        {
            _logger.LogDebug("> StartAsync started"); 
            await _bootManager.StartAsync();
            _logger.LogDebug("> StartAsync ended");
        }

        public async Task StopAsync()
        {
            _logger.LogDebug("> StopAsync started"); 
            await _bootManager.StopAsync();
            _logger.LogDebug("> StopAsync ended");
        }
    }
}