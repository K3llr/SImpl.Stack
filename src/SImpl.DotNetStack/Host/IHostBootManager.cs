using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SImpl.DotNetStack.Host
{
    public interface IHostBootManager
    {
        void PreInit();

        void ConfigureServices(IHostBuilder hostBuilder);

        void ConfigureHostBuilder(IHostBuilder hostBuilder);

        void ConfigureHost(IHost host);

        Task StartAsync();
        
        Task StopAsync();
    }
}