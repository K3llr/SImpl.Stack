using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Novicell.DotNetStack.Core;
using Novicell.DotNetStack.Extensions;
using Novicell.DotNetStack.Modules;

namespace Novicell.DotNetStack.Host
{
    public class DotNetStackHost : IHost
    {
        private readonly IDotNetStackRuntime _runtime;
        private readonly IHost _host;

        public DotNetStackHost(IDotNetStackRuntime runtime, IHost host)
        {
            _runtime = runtime;
            _host = host;
        }
        
        public void Dispose()
        {
            _host.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _host.StartAsync(cancellationToken);
            await _runtime.ModuleManager.EnabledModules.ForEachAsync<IStartableModule>(module => module.StartAsync());
        }

        public async Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _runtime.ModuleManager.EnabledModules.ForEachAsync<IStartableModule>(module => module.StopAsync());
            await _host.StopAsync(cancellationToken);
        }

        public IServiceProvider Services => _host.Services;
    }
}