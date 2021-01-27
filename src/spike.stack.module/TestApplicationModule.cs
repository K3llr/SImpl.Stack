using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Modules;
using spike.stack.app.Application;
using spike.stack.app.Domain;

namespace spike.stack.module
{
    public class TestApplicationModule : IApplicationModule, IServicesCollectionConfigureModule, IPreInitModule
    {
        public void Configure(ISImplApplicationBuilder builder)
        {
            
        }

        public string Name => nameof(TestApplicationModule);
        
        public void PreInit()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IGreetingAppService, GreetingAppService>();
            services.AddTransient<IGreetingService, SpanishGreetingService>();
            services.AddHostedService<GreetingHostedService>();
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