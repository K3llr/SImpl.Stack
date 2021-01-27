using System.Threading.Tasks;
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

        public async Task StartAsync()
        {
            await _bootManager.StartAsync<IGenericHostApplicationModule>();
        }

        public async Task StopAsync()
        {
            await _bootManager.StopAsync<IGenericHostApplicationModule>();
        }
    }
}