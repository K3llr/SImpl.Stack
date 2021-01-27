using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.GenericHost;
using SImpl.Hosts.GenericHost.Startup;
using SImpl.Runtime;
using spike.stack.module;

namespace spike.stack.console
{
    public class Startup : IGenericHostApplicationStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Works
            //services.AddScoped<IGreetingAppService, GreetingAppService>();
            //services.AddScoped<IGreetingService, SpanishGreetingService>();
            //services.AddHostedService<GreetingHostedService>();
        }

        public void ConfigureApplication(IGenericHostApplicationBuilder builder)
        {
            builder.UseAppModule<TestStackedApplicationModule>();
            
            builder.UseAppModule<ApplicationTestModule>();
            //stack.UseApplicationTestModule();
            builder.UseGenericHostAppModule<GenericHostApplicationTestModule>();
            //stack.UseGenericHostApplicationTestModule();
            
            // NOTE: Below is not supposed to work
            //stack.UseWebHostStackAppModule<WebHostApplicationTestModule>();
            //stack.UseWebApplicationTestModule();

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