using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Runtime.Application
{
    public class DotNetStackApplication : IDotNetStackApplication
    {
        private readonly IApplicationBootManager _bootManager;

        public DotNetStackApplication(IApplicationBootManager bootManager)
        {
            _bootManager = bootManager;
        }

        public async Task StartAsync()
        {
            await _bootManager.StartAsync();
        }

        public async Task StopAsync()
        {
            await _bootManager.StopAsync();
        }
    }
}