using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;
using spike.stack.app.Application;
using spike.stack.app.Domain;

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
            services.AddScoped<IGreetingAppService, GreetingAppService>();
            services.AddScoped<IGreetingService, SpanishGreetingService>();
            services.AddHostedService<GreetingHostedService>();
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