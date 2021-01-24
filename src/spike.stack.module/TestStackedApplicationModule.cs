using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Modules;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Modules.Dependencies;
using SImpl.DotNetStack.Runtime.ApplicationBuilders;

namespace spike.stack.module
{
    [DependsOn(typeof(TestApplicationModule))]
    public class TestStackedApplicationModule : IApplicationModule, IServicesCollectionConfigureModule, IPreInitModule
    {
        public void Configure(IDotNetStackApplicationBuilder builder)
        {
            builder.AttachNewOrGetConfiguredModule<TestApplicationModule>();
        }

        public string Name => nameof(TestStackedApplicationModule);
        public void PreInit()
        {
        }

        public void ConfigureServices(IServiceCollection services)
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