using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Modules.Dependencies;

namespace spike.stack.module
{
    [DependsOn(typeof(TestApplicationModule))]
    public class TestStackedApplicationModule : IApplicationModule, IServicesCollectionConfigureModule, IPreInitModule
    {
        public string Name => nameof(TestStackedApplicationModule);
        public void PreInit()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IDotNetStackApplicationBuilder builder)
        {
            builder.AttachNewOrGetConfiguredModule<TestApplicationModule>();
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