using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Extensions;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Core
{
    public class HostBootManager : IHostBootManager
    {
        private readonly IModuleManager _moduleManager;

        public HostBootManager(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        private IEnumerable<IDotNetStackModule> _bootSequence;
        private IEnumerable<IDotNetStackModule> BootSequence => _bootSequence ??= _moduleManager.BootSequence;

        public void PreInit()
        {
            BootSequence.ForEach<IPreInitModule>(module =>
            {
                module.PreInit();
            });
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            BootSequence.ForEach<IServicesCollectionConfigureModule>(module =>
            {
                hostBuilder.ConfigureServices((hostBuilderContext, services) => module.ConfigureServices(services));
            });
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            BootSequence.ForEach<IHostBuilderConfigureModule>(module =>
            {
                module.ConfigureHostBuilder(hostBuilder);
            });
        }

        public void ConfigureHost(IHost host)
        {
            BootSequence.ForEach<IHostConfigureModule>(module =>
            {
                module.ConfigureHost(host);
            });
            
            _moduleManager.SetModuleState(ModuleState.Configured);
        }

        public async Task StartAsync()
        {
            await BootSequence.ForEachAsync<IStartableModule>(module => module.StartAsync());
            
            _moduleManager.SetModuleState(ModuleState.Started);
        }

        public async Task StopAsync()
        {
            await BootSequence.ForEachAsync<IStartableModule>(module => module.StopAsync());
            
            _moduleManager.SetModuleState(ModuleState.Stopped);
        }
    }
}