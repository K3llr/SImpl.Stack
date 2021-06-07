using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.Application.Builders;
using SImpl.Modules;
using spike.stack.application;
using spike.stack.application.Domain;

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

        public Task StartAsync(IHost host)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(IHost host)
        {
            return Task.CompletedTask;
        }
    }
}