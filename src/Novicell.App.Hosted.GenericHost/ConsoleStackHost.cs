using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Novicell.App.Hosted.Modules;

namespace Novicell.App.Hosted.GenericHost
{
    public class ConsoleStackHost : IHost
    {
        private readonly IHost _host;
        private readonly NovicellApp _application;

        public ConsoleStackHost(IHost host, NovicellApp application)
        {
            _host = host;
            _application = application;
        }

        public void Dispose()
        {
            _host.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _host.StartAsync(cancellationToken);
            
            foreach (var startableModule in StartableModules)
            {
                await startableModule.StartAsync();
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var startableModule in StartableModules)
            {
                await startableModule.StopAsync();
            }
            
            await _host.StopAsync(cancellationToken);
        }

        public IServiceProvider Services => _host.Services;

        private IEnumerable<IAsyncStartableModule> StartableModules
        {
            get
            {
                foreach (var module in _application.StartupModules)
                {
                    if (module is IAsyncStartableModule startableModule)
                    {
                        yield return startableModule;
                    }
                }
            }
        }
    }
}