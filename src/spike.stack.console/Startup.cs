using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Runtime.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Configuration;
using SImpl.DotNetStack.Hosts.GenericHost.Extensions;
using spike.stack.app.Application;
using spike.stack.app.Domain;
using spike.stack.module;

namespace spike.stack.console
{
    public class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Works
            //services.AddScoped<IGreetingAppService, GreetingAppService>();
            //services.AddScoped<IGreetingService, SpanishGreetingService>();
            //services.AddHostedService<GreetingHostedService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDotNetStackGenericApp(stack =>
            {
                stack.Use<TestStackedApplicationModule>();
            });

            /*stackBuilder.UseNovicellConsoleApp(consoleApp =>
            {
               
                
                //consoleApp.UseHybridTestModule();
                //consoleApp.Use<LegacyStackModule>();

                consoleApp.UseDependencyInjection(di =>
                {
                    di.UseGenericHost();
                    
                    di.RegisterServices(container =>
                    {
                        container.RegisterHostedService<GreetingHostedService>();
                        
                        container.Register<IGreetingAppService, GreetingAppService>(Lifestyle.Singleton);
                        container.Register<IGreetingService, SpanishGreetingService>(Lifestyle.Singleton);
                    });
                });
            });*/
        }
    }
}