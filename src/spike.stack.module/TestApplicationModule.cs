using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace spike.stack.module
{
    public class TestApplicationModule : IApplicationModule, IServicesCollectionConfigureModule, IPreInitModule
    {
        public string Name => nameof(TestApplicationModule);
        
        public void PreInit()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IDotNetStackApplicationBuilder builder)
        {
        }

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}