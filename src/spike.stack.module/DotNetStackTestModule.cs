using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.Modules;

namespace spike.stack.module
{
    public class DotNetStackTestModule : IStartableModule, IHostBuilderConfigureModule, IHostConfigureModule, IServicesCollectionConfigureModule
    {
        public void Init()
        {
            // Console.WriteLine($"Init: {Name}");
        }

        public string Name => nameof(DotNetStackTestModule);
        
        public Task StartAsync()
        {
            // Console.WriteLine($"Start async: {Name}");

            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            // Console.WriteLine($"Stop async: {Name}");
            
            return Task.CompletedTask;
        }

        public void ConfigureHost(IHost host)
        {
            // Console.WriteLine($"Configure host object: {host.GetType().Name}");
        }

        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            // Console.WriteLine($"Configure host builder: {hostBuilder.GetType().Name}");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Console.WriteLine($"Configure services: {services.GetType().Name}");
        }
    }
}