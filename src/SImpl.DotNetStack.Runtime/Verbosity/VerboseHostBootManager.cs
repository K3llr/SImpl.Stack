using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.DotNetStack.Runtime.Host;

namespace SImpl.DotNetStack.Runtime.Verbosity
{
    public class VerboseHostBootManager : IHostBootManager
    {
        private readonly IHostBootManager _bootManager;
        private readonly IModuleManager _moduleManager;
        private readonly IBootSequenceFactory _bootSequenceFactory;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseHostBootManager(IHostBootManager bootManager, IModuleManager moduleManager, IBootSequenceFactory bootSequenceFactory, ILogger<VerboseHost> logger)
        {
            _bootManager = bootManager;
            _moduleManager = moduleManager;
            _bootSequenceFactory = bootSequenceFactory;
            _logger = logger;
        }

        public IEnumerable<IPreInitModule> PreInit()
        {
            _logger.LogDebug(" HostBootManager > PreInit > started");
            var processed = _bootManager.PreInit().ToArray();
            _logger.LogDebug(" HostBootManager > PreInit > Module PreInit sequence");
            foreach (var module in processed)
            {
                _logger.LogDebug($"  - {module.Name}");
            }
            _logger.LogDebug(" HostBootManager > PreInit > Module boot sequence");
            foreach (var module in _bootSequenceFactory.New())
            {
                _logger.LogDebug($"  - {module.Name}");
            }
            _logger.LogDebug(" HostBootManager > PreInit > ended");

            return processed;
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            _logger.LogDebug(" HostBootManager > ConfigureServices > started");
            _bootManager.ConfigureServices(hostBuilder);
            _logger.LogDebug(" HostBootManager > ConfigureServices > ended");
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            _logger.LogDebug(" HostBootManager > ConfigureHostBuilder > started");
            _bootManager.ConfigureHostBuilder(hostBuilder);
            _logger.LogDebug(" HostBootManager > ConfigureHostBuilder > ended");
        }

        public void ConfigureHost(IHost host)
        {
            _logger.LogDebug(" HostBootManager > ConfigureHost > started");
            _bootManager.ConfigureHost(host);
            _logger.LogDebug(" HostBootManager > ConfigureHost > ended");
        }

        public async Task StartAsync()
        {
            _logger.LogDebug(" HostBootManager > StartAsync > started"); 
            await _bootManager.StartAsync();
            _logger.LogDebug(" HostBootManager > StartAsync > ended");
        }

        public async Task StopAsync()
        {
            _logger.LogDebug(" HostBootManager > StopAsync > started"); 
            await _bootManager.StopAsync();
            _logger.LogDebug(" HostBootManager > StopAsync > ended");
        }
    }
}