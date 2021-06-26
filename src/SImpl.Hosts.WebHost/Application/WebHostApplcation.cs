using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
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

        public async Task StartAsync(IHost host)
        {
            await _bootManager.StartAsync<IWebHostApplicationModule>(host);
        }

        public async Task StopAsync(IHost host)
        {
            await _bootManager.StopAsync<IWebHostApplicationModule>(host);
        }
    }
}