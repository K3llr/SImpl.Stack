using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.Modules;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.WebHost.Application
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