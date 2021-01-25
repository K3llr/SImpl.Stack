using System.Threading.Tasks;
using SImpl.Stack.Application;
using SImpl.Stack.Modules;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Hosts.GenericHost.Application
{
    public class GenericHostApplication : IDotNetStackApplication
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