using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Runtime.Verbosity
{
    public class VerboseApplication : IDotNetStackApplication
    {
        private readonly IDotNetStackApplication _application;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseApplication(IDotNetStackApplication application, ILogger<VerboseHost> logger)
        {
            _application = application;
            _logger = logger;
        }
        public async Task StartAsync()
        {
            _logger.LogDebug("   Application > Application starting");
            await _application.StartAsync();
            _logger.LogDebug("   Application > Application started");
        }

        public async Task StopAsync()
        {
            _logger.LogDebug("   Application > Application stopping");
            await _application.StopAsync();
            _logger.LogDebug("   Application > Application Stopped");
        }
    }
}