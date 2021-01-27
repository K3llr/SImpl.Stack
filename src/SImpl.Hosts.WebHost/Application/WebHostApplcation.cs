using System.Threading.Tasks;
using SImpl.Application;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Hosts.WebHost.Application
{
    public class WebHostApplcation : ISImplApplication
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