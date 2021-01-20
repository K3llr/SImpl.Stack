using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.DotNetStack.Runtime.Extensions;

namespace SImpl.DotNetStack.Runtime.Host
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

        public  IEnumerable<IPreInitModule> PreInit()
        {
            var preInitModules = new List<IPreInitModule>();
            
            // NOTE: Please do no try and optimize the following loop. Especially not to put Config.EnabledModules into a variable before the loop or to use an foreach loop
            // because the collection is expanded inside the loop due to recursive nature of the stack.
            for (var i = 0; i < _moduleManager.EnabledModules.Count; i++)
            {
                var module = _moduleManager.EnabledModules[i] as IPreInitModule;
                module?.PreInit();
                
                preInitModules.Add(module);
            }

            return preInitModules;
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, services) =>
            {
                BootSequence.ForEach<IServicesCollectionConfigureModule>(module => module.ConfigureServices(services));
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
            await BootSequence.Reverse().ForEachAsync<IStartableModule>(module => module.StopAsync());
            
            _moduleManager.SetModuleState(ModuleState.Stopped);
        }
    }
}