using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Hosts.WebHost.Modules;
using SImpl.DotNetStack.Modules;
using spike.stack.app.Application;
using spike.stack.app.Domain;

namespace spike.stack.module
{
    public class GreetingsWebModule : IAspNetApplicationModule, IServicesCollectionConfigureModule
    {
        private IGreetingAppService _greetingAppService;

        public void Configure(IWebHostApplicationBuilder builder)
        {
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _greetingAppService = app.ApplicationServices.GetService<IGreetingAppService>();
        }

        public string Name => nameof(GreetingsWebModule);

        public void ConfigureServices(IServiceCollection services)
        {
            // not called before configure
            services.AddTransient<IGreetingAppService, GreetingAppService>();
            services.AddTransient<IGreetingService, SpanishGreetingService>();
        }

        public Task StartAsync()
        {
            Console.WriteLine(_greetingAppService.SayHi("Keller"));
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine(_greetingAppService.SayBye("Keller"));
            return Task.CompletedTask;
        }
    }
}