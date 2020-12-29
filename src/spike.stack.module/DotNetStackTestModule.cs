using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.App.AppBuilders;
using Novicell.DotNetStack.Modules;

namespace spike.stack.module
{
    public class Test01Module : IStartableModule, IHostConfigureModule, IHostObjectConfigureModule, IServicesCollectionConfigureModule
    {
        public void Configure(INovicellAppBuilder appBuilder)
        {
            Console.WriteLine($"Configure: {Name}");
        }

        public void Init()
        {
            Console.WriteLine($"Init: {Name}");
        }

        public string Name => "Test 01 module";
        
        public Task StartAsync()
        {
            Console.WriteLine($"Start async: {Name}");

            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine($"Stop async: {Name}");
            
            return Task.CompletedTask;
        }

        public void ConfigureHost(IHost host)
        {
            Console.WriteLine($"Configure host object: {host.GetType().Name}");
        }

        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            Console.WriteLine($"Configure host builder: {hostBuilder.GetType().Name}");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine($"Configure services: {services.GetType().Name}");
        }
    }
}