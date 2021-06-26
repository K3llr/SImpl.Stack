using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Host.Builders;
using SImpl.Modules;

namespace SImpl.Runtime.Core
{
    public interface IHostBootManager
    {
        IEnumerable<IPreInitModule> PreInit();

        void ConfigureServices(ISImplHostBuilder hostBuilder);

        void ConfigureHostBuilder(ISImplHostBuilder hostBuilder);

        void ConfigureHost(IHost host);

        Task StartAsync(IHost host);
        
        Task StopAsync(IHost host);
    }
}