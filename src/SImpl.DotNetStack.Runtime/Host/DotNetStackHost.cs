using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Runtime.Host
{
    public class DotNetStackHost : IHost
    {
        private readonly IHost _host;
        private readonly IHostBootManager _bootManager;

        public DotNetStackHost(IHost host, IHostBootManager bootManager)
        {
            _host = host;
            _bootManager = bootManager;
        }
        
        public void Dispose()
        {
            _host.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {  
            await _host.StartAsync(cancellationToken);
            await _bootManager.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _bootManager.StopAsync();
            await _host.StopAsync(cancellationToken);
        }

        public IServiceProvider Services => _host.Services;
    }
}