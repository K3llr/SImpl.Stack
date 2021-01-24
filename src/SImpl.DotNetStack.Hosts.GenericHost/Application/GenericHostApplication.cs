using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Modules;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.GenericHost.Application
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