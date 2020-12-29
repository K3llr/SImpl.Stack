using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Extensions;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Host
{
    public class DotNetStackHost : IHost
    {
        private readonly IHost _host;
        private readonly IModuleManager _moduleManager;

        public DotNetStackHost(IHost host, IModuleManager moduleManager)
        {
            _host = host;
            _moduleManager = moduleManager;
        }
        
        public void Dispose()
        {
            _host.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _moduleManager.EnabledModules.ForEachAsync<IStartableModule>(module => module.StartAsync());
            await _host.StartAsync(cancellationToken);
            _moduleManager.SetModuleState(ModuleState.Started);
        }

        public async Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _host.StopAsync(cancellationToken);
            await _moduleManager.EnabledModules.ForEachAsync<IStartableModule>(module => module.StopAsync());
            
            _moduleManager.SetModuleState(ModuleState.Stopped);
        }

        public IServiceProvider Services => _host.Services;
    }
}