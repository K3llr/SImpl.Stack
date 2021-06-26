using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Application;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Hosts.GenericHost.Application
{
    public class GenericHostApplication : ISImplApplication
    {
        private readonly IApplicationBootManager _bootManager;

        public GenericHostApplication(IApplicationBootManager bootManager)
        {
            _bootManager = bootManager;
        }

        public async Task StartAsync(IHost host)
        {
            await _bootManager.StartAsync<IGenericHostApplicationModule>(host);
        }

        public async Task StopAsync(IHost host)
        {
            await _bootManager.StopAsync<IGenericHostApplicationModule>(host);
        }
    }
}