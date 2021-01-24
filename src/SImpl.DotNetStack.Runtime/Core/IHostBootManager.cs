using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Runtime.Core
{
    public interface IHostBootManager
    {
        IEnumerable<IPreInitModule> PreInit();

        void ConfigureServices(IHostBuilder hostBuilder);

        void ConfigureHostBuilder(IHostBuilder hostBuilder);

        void ConfigureHost(IHost host);

        Task StartAsync();
        
        Task StopAsync();
    }
}