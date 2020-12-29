using System.Collections.Generic;
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

        public IEnumerable<IDotNetStackModule> Modules => _moduleManager.EnabledModules; // TODO: Sort by dependencies

        public void PreInit()
        {
            Modules.ForEach<IPreInitModule>(module =>
            {
                module.PreInit();
            });
        }

        public void ConfigureServices(IHostBuilder hostBuilder)
        {
            Modules.ForEach<IServicesCollectionConfigureModule>(module =>
            {
                hostBuilder.ConfigureServices((hostBuilderContext, services) => module.ConfigureServices(services));
            });
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            Modules.ForEach<IHostBuilderConfigureModule>(module =>
            {
                module.ConfigureHostBuilder(hostBuilder);
            });
        }

        public void ConfigureHost(IHost host)
        {
            Modules.ForEach<IHostConfigureModule>(module =>
            {
                module.ConfigureHost(host);
            });
        }
    }
}