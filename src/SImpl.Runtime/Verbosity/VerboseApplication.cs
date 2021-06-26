using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Application;

namespace SImpl.Runtime.Verbosity
{
    public class VerboseApplication : ISImplApplication
    {
        private readonly ISImplApplication _application;
        private readonly ILogger _logger;

        public VerboseApplication(ISImplApplication application, ILogger logger)
        {
            _application = application;
            _logger = logger;
        }
        public async Task StartAsync(IHost host)
        {
            _logger.LogDebug("Application > Application starting");
            await _application.StartAsync(host);
            _logger.LogDebug("Application > Application started");
        }

        public async Task StopAsync(IHost host)
        {
            _logger.LogDebug("Application > Application stopping");
            await _application.StopAsync(host);
            _logger.LogDebug("Application > Application Stopped");
        }
    }
}