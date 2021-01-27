using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SImpl.Application.Builders;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Verbosity
{
    public class VerboseApplicationBootManager : IApplicationBootManager
    {
        private readonly IApplicationBootManager _bootManager;
        private readonly ILogger<SImply> _logger;

        public VerboseApplicationBootManager(IApplicationBootManager bootManager, ILogger<SImply> logger)
        {
            _bootManager = bootManager;
            _logger = logger;
        }
        
        public IEnumerable<TApplicationModule> Configure<TApplicationModule, TApplicationBuilder>(TApplicationBuilder appBuilder)
            where TApplicationModule : class, IApplicationModule<TApplicationBuilder>
            where TApplicationBuilder : ISImplApplicationBuilder
        {
            _logger.LogDebug("    ApplicationBootManager > Configure > started");
            var processed = _bootManager.Configure<TApplicationModule, TApplicationBuilder>(appBuilder).ToArray();
            _logger.LogDebug("    ApplicationBootManager > Configure > ended");

            return processed;
        }

        public async Task StartAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule
        {
            _logger.LogDebug("    ApplicationBootManager > StartAsync > started");
            await _bootManager.StartAsync<TApplicationModule>();
            _logger.LogDebug("    ApplicationBootManager > StartAsync > ended");
        }

        public async Task StopAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule
        {
            _logger.LogDebug("    ApplicationBootManager > StopAsync > started");
            await _bootManager.StopAsync<TApplicationModule>();
            _logger.LogDebug("    ApplicationBootManager > StopAsync > ended");
        }
    }
}