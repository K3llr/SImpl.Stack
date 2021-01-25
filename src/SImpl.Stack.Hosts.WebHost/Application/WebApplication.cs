using System.Threading.Tasks;
using SImpl.Stack.Application;
using SImpl.Stack.Modules;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Hosts.WebHost.Application
{
    public class WebHostApplcation : IDotNetStackApplication
    {
        private readonly IApplicationBootManager _bootManager;

        public WebHostApplcation(IApplicationBootManager bootManager)
        {
            _bootManager = bootManager;
        }

        public async Task StartAsync()
        {
            await _bootManager.StartAsync<IWebHostApplicationModule>();
        }

        public async Task StopAsync()
        {
            await _bootManager.StopAsync<IWebHostApplicationModule>();
        }
    }
}