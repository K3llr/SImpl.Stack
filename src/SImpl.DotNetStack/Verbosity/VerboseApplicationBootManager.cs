using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

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
        
        public IEnumerable<IApplicationModule> Configure(IDotNetStackApplicationBuilder appBuilder)
        {
            _logger.LogDebug("    ApplicationBootManager > Configure > started");
            var processed = _bootManager.Configure(appBuilder).ToArray();
            _logger.LogDebug("    ApplicationBootManager > Configure > ended");

            return processed;
        }

        public async Task StartAsync()
        {
            _logger.LogDebug("    ApplicationBootManager > StartAsync > started");
            await _bootManager.StartAsync();
            _logger.LogDebug("    ApplicationBootManager > StartAsync > ended");
        }

        public async Task StopAsync()
        {
            _logger.LogDebug("    ApplicationBootManager > StopAsync > started");
            await _bootManager.StopAsync();
            _logger.LogDebug("    ApplicationBootManager > StopAsync > ended");
        }
    }
}