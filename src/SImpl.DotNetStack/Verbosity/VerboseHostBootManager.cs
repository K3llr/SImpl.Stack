using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseHostBootManager : IHostBootManager
    {
        private readonly IHostBootManager _bootManager;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseHostBootManager(IHostBootManager bootManager, ILogger<VerboseHost> logger)
        {
            _bootManager = bootManager;
            _logger = logger;
        }

        public IEnumerable<IDotNetStackModule> Modules => _bootManager.Modules;

        public void PreInit()
        {
            _logger.LogDebug("PreInit started");
            _bootManager.PreInit();
            _logger.LogDebug("PreInit ended");
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            _logger.LogDebug("ConfigureServices started");
            _bootManager.ConfigureServices(hostBuilder);
            _logger.LogDebug("ConfigureServices ended");
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            _logger.LogDebug("ConfigureHostBuilder started");
            _bootManager.ConfigureHostBuilder(hostBuilder);
            _logger.LogDebug("ConfigureHostBuilder ended");
        }

        public void ConfigureHost(IHost host)
        {
            _logger.LogDebug("ConfigureHost started");
            _bootManager.ConfigureHost(host);
            _logger.LogDebug("ConfigureHost ended");
        }
    }
}